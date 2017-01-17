using EasyLearn.Helpers;
using EasyLearn.Interfaces;
using EasyLearn.Models;
using EasyLearn.Services;
using FormsToolkit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace EasyLearn.Pages
{
    public partial class FileBrowser : ContentPage
    {
        public FileBrowser()
        {
            InitializeComponent();
            UpdateFileList();
        }

        async void Save(object sender, EventArgs args)
        {
            string filename = fileNameEntry.Text;
            if (String.IsNullOrEmpty(filename)) return;
            // если файл существует
            if (await DependencyService.Get<IFileWorker>().ExistsAsync(filename))
            {
                // запрашиваем разрешение на перезапись
                bool isRewrited = await DisplayAlert("Подверждение", "Файл уже существует, перезаписать его?", "Да", "Нет");
                if (isRewrited == false) return;
            }
            // перезаписываем файл
            var items = await ServiceManager.SqliteService.WordManager.readAllByCurrentLanguage(ServiceManager.SqliteService.Current.Id, ServiceManager.SqliteService.CurrentTranslation.Id);
            string output = JsonConvert.SerializeObject(items);
            await DependencyService.Get<IFileWorker>().SaveTextAsync(fileNameEntry.Text, output);
            // обновляем список файлов
            UpdateFileList();
        }


        async void FileSelect(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem == null) return;
            // получаем выделенный элемент
            string filename = (string)args.SelectedItem;
            // загружем текст в текстовое поле
            try
            {
                string input = await DependencyService.Get<IFileWorker>().LoadTextAsync((string)args.SelectedItem);
                List<Word> words = JsonConvert.DeserializeObject<List<Word>>(input);
                textEditor.Text = input;
                //await ServiceManager.SqliteService.WordManager.createMultiple(words);
                //await Navigation.PopAsync();

            }
            catch (Exception ex)
            {
                MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.DisplayAlert, new MessagingServiceAlert()
                {
                    Title = Titles.ERROR,
                    Message = ex.Message,
                    Cancel = Titles.CANCEL
                });
            }

        }
        async void Delete(object sender, EventArgs args)
        {
            // получаем имя файла
            string filename = (string)((MenuItem)sender).BindingContext;
            // удаляем файл из списка
            await DependencyService.Get<IFileWorker>().DeleteAsync(filename);
            // обновляем список файлов
            UpdateFileList();
        }
        // обновление списка файлов
        async void UpdateFileList()
        {
            // получаем все файлы
            filesList.ItemsSource = await DependencyService.Get<IFileWorker>().GetFilesAsync();
            // снимаем выделение
            filesList.SelectedItem = null;
        }
    }
}
