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
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var items = await LocalService.SqliteService.LanguageManager.readAll();
            languageView.ItemsSource = items;
            languageTranslationView.ItemsSource = items;
            if (LocalService.SqliteService.Current != null && LocalService.SqliteService.CurrentTranslation != null)
            {
                languageCurrent.Text = Constants.CURRENT + ": " + LocalService.SqliteService.Current.Name + " - " + LocalService.SqliteService.CurrentTranslation.Name;
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
                        wordLanguage.Current = 1;
                        wordTranslation.CurrentTranslation = 1;
                        await LocalService.SqliteService.LanguageManager.resetFlags();
                        await LocalService.SqliteService.LanguageManager.update(wordLanguage);
                        await LocalService.SqliteService.LanguageManager.update(wordTranslation);
                        LocalService.SqliteService.Current = wordLanguage;
                        LocalService.SqliteService.CurrentTranslation = wordTranslation;
                        MainPage.Main.Detail = new NavigationPage(new HomePage());
                    })
                });
             
            }
        }
        public void AddActivated(object sender, EventArgs arg)
        {
            Navigation.PushAsync(new EditLanguagePage() { Title = Constants.ADD_LANGUAGE });
        }
    }
}
