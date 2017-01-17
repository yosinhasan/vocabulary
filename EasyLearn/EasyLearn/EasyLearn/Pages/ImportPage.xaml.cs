using EasyLearn.Helpers;
using EasyLearn.Interfaces;
using EasyLearn.Models;
using EasyLearn.Services;
using FormsToolkit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace EasyLearn.Pages
{
    public partial class ImportPage : ContentPage
    {
        public ImportPage()
        {
            InitializeComponent();
            UpdateFileList();
        }
        public async void SaveActivated(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        private void ImportClicked(object sender, EventArgs args)
        {
            if (filesList.SelectedItem == null)
            {
                MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.DisplayAlert, new MessagingServiceAlert()
                {
                    Title = Titles.ERROR,
                    Message = Messages.FILE_ITEM_NOT_SELECTED,
                    Cancel = Titles.CANCEL
                });
                return;
            }
            string filename = (string)filesList.SelectedItem;
            filesList.SelectedItem = null;
            MessagingService.Current.SendMessage<MessagingServiceQuestion>(MessageKeys.DisplayQuestion, new MessagingServiceQuestion()
            {
                Title = Messages.SAVE_DATA,
                Question = null,
                Positive = Constants.YES,
                Negative = Constants.NO,
                OnCompleted = new Action<bool>(async result =>
                {
                    if (!result) return;
                    try
                    {
                        string input = await DependencyService.Get<IFileWorker>().LoadTextAsync(filename);
                        input = input.ToString();
                        List<Word> words = JsonConvert.DeserializeObject<List<Word>>(input);
                        List<Word> newWords = new List<Word>();
                        foreach (Word word in words)
                        {
                            if (!await ServiceManager.SqliteService.WordManager.isExists(word.Keyword, ServiceManager.SqliteService.Current.Id, ServiceManager.SqliteService.CurrentTranslation.Id))
                            {
                                word.LangId = ServiceManager.SqliteService.Current.Id;
                                word.TranslationLangId = ServiceManager.SqliteService.CurrentTranslation.Id;
                                newWords.Add(word);
                            }
                        }
                        words = null;
                        if (newWords.Count > 0)
                        {
                            await ServiceManager.SqliteService.WordManager.createMultiple(newWords);
                            MessagingService.Current.SendMessage(MessageKeys.DisplayAlert, new MessagingServiceAlert()
                            {
                                Title = Titles.SUCCESS,
                                Message = Messages.IMPORTED,
                                Cancel = Titles.CANCEL
                            });
                        }
                        else
                        {
                            MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.DisplayAlert, new MessagingServiceAlert()
                            {
                                Title = Titles.SUCCESS,
                                Message = Messages.NOTHING_TO_IMPORT,
                                Cancel = Titles.CANCEL
                            });
                        }
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
                })
            });
        }
        private async void Delete(object sender, EventArgs args)
        {
            string filename = (string)((MenuItem)sender).BindingContext;
            await DependencyService.Get<IFileWorker>().DeleteAsync(filename);
            UpdateFileList();
        }
        private async void UpdateFileList()
        {
            filesList.ItemsSource = await DependencyService.Get<IFileWorker>().GetFilesAsync();
            filesList.SelectedItem = null;
        }
    }
}
