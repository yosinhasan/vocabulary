using EasyLearn.Helpers;
using EasyLearn.Interfaces;
using EasyLearn.Services;
using FormsToolkit;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EasyLearn.Pages
{
    public partial class ExportPage : ContentPage
    {
        public ExportPage()
        {
            InitializeComponent();
        }
        private void ExportClicked(object sender, EventArgs args)
        {
            string filename = fileNameEntry.Text;
            if (String.IsNullOrEmpty(filename))
            {
                MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.DisplayAlert, new MessagingServiceAlert()
                {
                    Title = Titles.ERROR,
                    Message = Messages.FIELD_EMPTY,
                    Cancel = Titles.CANCEL
                });
                return;
            }
            MessagingService.Current.SendMessage(MessageKeys.DisplayQuestion, new MessagingServiceQuestion()
            {
                Title = Messages.EXPORT_DATA,
                Question = null,
                Positive = Constants.YES,
                Negative = Constants.NO,
                OnCompleted = new Action<bool>(async result =>
                {
                    if (!result) return;
                    if (await DependencyService.Get<IFileWorker>().ExistsAsync(filename))
                    {
                        MessagingService.Current.SendMessage(MessageKeys.DisplayQuestion, new MessagingServiceQuestion()
                        {
                            Title = Messages.REPLACE_EXISTS,
                            Question = null,
                            Positive = Constants.YES,
                            Negative = Constants.NO,
                            OnCompleted = new Action<bool>(async res =>
                            {
                                if (!res) return;
                                await export();
                            })
                        });
                    }
                    else
                    {
                        await export();
                    }

                })
            });
        }
        private async Task export()
        {
            var items = await ServiceManager.SqliteService.WordManager.readAllByCurrentLanguage(ServiceManager.SqliteService.Current.Id, ServiceManager.SqliteService.CurrentTranslation.Id);
            if (items == null)
            {
                MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.DisplayAlert, new MessagingServiceAlert()
                {
                    Title = Titles.ERROR,
                    Message = Messages.NOTHING_TO_EXPORT,
                    Cancel = Titles.CANCEL
                });
            }
            else
            {
                string output = JsonConvert.SerializeObject(items);
                await DependencyService.Get<IFileWorker>().SaveTextAsync(fileNameEntry.Text, output);
                MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.DisplayAlert, new MessagingServiceAlert()
                {
                    Title = Titles.SUCCESS,
                    Message = Messages.EXPORTED_DATA,
                    Cancel = Titles.CANCEL
                });
            }
        }
        private async void ChooseClicked(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new FileBrowser());
        }
    }
}
