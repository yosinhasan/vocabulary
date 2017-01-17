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
    public partial class WordPage : ContentPage
    {
        public WordPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (ServiceManager.SqliteService.Current == null || ServiceManager.SqliteService.CurrentTranslation == null)
            {
                MainPage.Main.Detail = new NavigationPage(new SettingsPage());
            }
            else
            {
                Title = ServiceManager.SqliteService.Current.Name.Substring(0, 2).ToUpper() + " - " + ServiceManager.SqliteService.CurrentTranslation.Name.Substring(0, 2).ToUpper();
            }
        }
        public void SaveActivated(object sender, EventArgs e)
        {
            string word = formLang.Text;
            string translation = formTransWord.Text;
            string transcript = formTranscript.Text;
            if (word == null || word.Trim() == "" || translation == null || translation.Trim() == "")
            {
                MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.DisplayAlert, new MessagingServiceAlert()
                {
                    Title = Titles.ERROR,
                    Message = Messages.FIELD_EMPTY,
                    Cancel = Titles.CANCEL
                });
            }
            else if (ServiceManager.SqliteService.Current == null || ServiceManager.SqliteService.CurrentTranslation == null)
            {
                MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.DisplayAlert, new MessagingServiceAlert()
                {
                    Title = Titles.ERROR,
                    Message = Messages.LANGUAGE_ITEM_NOT_SELECTED,
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
                        string keyword = word.Trim().ToLower();
                        long langId = ServiceManager.SqliteService.Current.Id;
                        long tranLangId = ServiceManager.SqliteService.CurrentTranslation.Id;
                        bool isExists = await ServiceManager.SqliteService.WordManager.isExists(keyword, langId, tranLangId);
                        if (!isExists)
                        {
                            Word newWord = await ServiceManager.SqliteService.WordManager.create(new Word
                            {
                                Keyword = keyword,
                                LangId = langId,
                                TranslationLangId = tranLangId,
                                Text = translation.Trim().ToLower(),
                                Transript = (transcript != null) ? transcript.Trim().ToLower() : ""
                            });
                        }
                        else
                        {
                            MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.DisplayAlert, new MessagingServiceAlert()
                            {
                                Title = Titles.ERROR,
                                Message = Messages.WORD_EXISTS,
                                Cancel = Titles.CANCEL
                            });
                        }

                        await Navigation.PopAsync();
                    })
                });
            }
        }
    }
}
