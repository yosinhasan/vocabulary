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

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var items = await LocalService.SqliteService.LanguageManager.readAll();
            languageView.ItemsSource = items;
            languageTranslationView.ItemsSource = items;
        }

        public void SaveActivated(object sender, EventArgs e)
        {
            string word = formLang.Text;
            string translation = formTransWord.Text;
            string transcript = formTranscript.Text;
            Language wordLanguage = (Language)languageView.SelectedItem;
            Language wordTranslation = (Language)languageTranslationView.SelectedItem;
            if (word == null || word.Trim() == "" || translation == null || translation.Trim() == "")
            {
                MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.DisplayAlert, new MessagingServiceAlert()
                {
                    Title = Titles.ERROR,
                    Message = Messages.FIELD_EMPTY,
                    Cancel = Titles.CANCEL
                });
            }
            else if (wordLanguage == null || wordTranslation == null)
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
                        string keyword = word.Trim().ToLower();
                        long langId = wordLanguage.Id;
                        Word oldWord = await LocalService.SqliteService.WordManager.readByKeywordAndLangId(keyword, langId);
                        if (oldWord == null)
                        {
                            Word newWord = await LocalService.SqliteService.WordManager.create(new Word
                            {
                                Keyword = keyword,
                                LangId = langId
                            });
                            oldWord = newWord;
                        }
                        if (oldWord != null)
                        {
                            translation = translation.Trim().ToLower();
                            transcript = (transcript != null) ? transcript.Trim().ToLower() : "-";
                            Translation item = await LocalService.SqliteService.VocabularyManager.readByWordIdAndLangId(oldWord.Id, wordTranslation.Id);
                            if (item != null)
                            {
                                item.Text = translation;
                                item.Transript = transcript;
                                await LocalService.SqliteService.VocabularyManager.update(item);
                            }
                            else
                            {
                                await LocalService.SqliteService.VocabularyManager.create(new Translation
                                {
                                    LangId = wordTranslation.Id,
                                    Text = translation,
                                    Transript = transcript,
                                    WordId = oldWord.Id
                                });
                            }
                        }
                        await Navigation.PopAsync();
                    })
                });

            }
        }
    }
}
