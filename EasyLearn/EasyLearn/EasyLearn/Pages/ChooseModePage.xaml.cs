using EasyLearn.Helpers;
using EasyLearn.Models;
using EasyLearn.Services;
using FormsToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace EasyLearn.Pages
{
    public partial class ChooseModePage : ContentPage
    {
        public ChooseModePage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var items = await ServiceManager.SqliteService.LanguageManager.readAll();
            languageView.ItemsSource = items;
            languageTranslationView.ItemsSource = items;
            if (ServiceManager.SqliteService.Current != null && ServiceManager.SqliteService.CurrentTranslation != null)
            {
                languageCurrent.Text = Constants.CURRENT + ": " + ServiceManager.SqliteService.Current.Name + " - " + ServiceManager.SqliteService.CurrentTranslation.Name;
            }
        }
        public void SaveActivated(object sender, EventArgs e)
        {
            Language wordLanguage = (Language)languageView.SelectedItem;
            Language wordTranslation = (Language)languageTranslationView.SelectedItem;
            if (wordLanguage == null || wordTranslation == null)
            {
                MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.DisplayAlert, new MessagingServiceAlert()
                {
                    Title = Titles.ERROR,
                    Message = Messages.LANGUAGE_ITEM_NOT_SELECTED,
                    Cancel = Titles.CANCEL
                });
            }
            else if (wordLanguage.Id == wordTranslation.Id)
            {
                MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.DisplayAlert, new MessagingServiceAlert()
                {
                    Title = Titles.ERROR,
                    Message = Messages.LANGUAGES_MUST_NOT_MATCH,
                    Cancel = Titles.CANCEL
                });
            }
            else
            {
                MessagingService.Current.SendMessage<MessagingServiceQuestion>(MessageKeys.DisplayQuestion, new MessagingServiceQuestion()
                {
                    Title = Messages.SAVE_DATA,
                    Question = null,
                    Positive = Constants.YES,
                    Negative = Constants.NO,
                    OnCompleted = new Action<bool>(async result =>
                    {
                        if (!result) return;
                        wordLanguage.Current = 1;
                        wordTranslation.CurrentTranslation = 1;
                        await ServiceManager.SqliteService.LanguageManager.resetFlags();
                        await ServiceManager.SqliteService.LanguageManager.update(wordLanguage);
                        await ServiceManager.SqliteService.LanguageManager.update(wordTranslation);
                        ServiceManager.SqliteService.Current = wordLanguage;
                        ServiceManager.SqliteService.CurrentTranslation = wordTranslation;
                        await Navigation.PopAsync();
                    })
                });

            }
        }
    }
}
