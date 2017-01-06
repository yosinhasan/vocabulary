using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyLearn.Models;
using Xamarin.Forms;
using EasyLearn.Services;
using EasyLearn.Helpers;
using FormsToolkit;

namespace EasyLearn.Pages
{
    public partial class DetailWordPage : ContentPage
    {
        private Word item;
        private Translation translation;
        public DetailWordPage(Word item)
        {
            InitializeComponent();
            this.item = item;

        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (this.item != null)
            {
                Translation translation = await LocalService.SqliteService.VocabularyManager.readByWordId(item.Id, LocalService.SqliteService.CurrentTranslation.Id);
                this.translation = translation;
                if (translation != null)
                {
                    language.Text = Constants.FROM + " " + LocalService.SqliteService.Current.Name + " " + Constants.TO + " " + LocalService.SqliteService.CurrentTranslation.Name;
                    word.Text = item.Keyword;
                    transcript.Text = translation.Transript;
                    translationWord.Text = translation.Text;
                }
                else
                {
                    MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.DisplayAlert, new MessagingServiceAlert()
                    {
                        Title = Titles.ERROR,
                        Message = Messages.TRANSLATION_NOT_FOUND,
                        Cancel = Titles.CANCEL
                    });
                }
            }
            else
            {
                MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.DisplayAlert, new MessagingServiceAlert()
                {
                    Title = Titles.ERROR,
                    Message = Messages.WORD_NOT_FOUND,
                    Cancel = Titles.CANCEL
                });
            }
        }
        public async void SaveActivated(object sender, EventArgs e)
        {
            string response = await DisplayActionSheet(Titles.SAVE, Titles.CANCEL, Messages.SAVE_DATA, new string[] { Constants.YES, Constants.NO });
            if (response.Equals(Constants.YES))
            {
                string translationKeyword = word.Text;
                string translation = translationWord.Text;
                string translationTranscript = transcript.Text;

                if (translationKeyword == null || translationKeyword.Trim() == "" || translation == null || translation.Trim() == "")
                {
                    MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.DisplayAlert, new MessagingServiceAlert()
                    {
                        Title = Titles.ERROR,
                        Message = Messages.FIELD_EMPTY,
                        Cancel = Titles.CANCEL
                    });
                }
                else
                {
                    string keyword = translationKeyword.Trim().ToLower();
                    item.Keyword = keyword;
                    await LocalService.SqliteService.WordManager.update(item);
                    this.translation.Text = translation.Trim().ToLower();
                    this.translation.Transript = (translationTranscript != null) ? translationTranscript.Trim().ToLower() : "";
                    await LocalService.SqliteService.VocabularyManager.update(this.translation);
                    this.translation = null;
                    this.item = null;
                    await Navigation.PopAsync();
                }

            }
        }
        public void DeleteActivated(object sender, EventArgs e)
        {
            MessagingService.Current.SendMessage<MessagingServiceQuestion>(MessageKeys.DisplayQuestion, new MessagingServiceQuestion()
            {
                Title = Messages.DELETE_ACTION,
                Question = null,
                Positive = Constants.YES,
                Negative = Constants.NO,
                OnCompleted = new Action<bool>(async result =>
                {

                    await LocalService.SqliteService.VocabularyManager.delete(item.Id, LocalService.SqliteService.CurrentTranslation.Id);
                    await LocalService.SqliteService.WordManager.safeDelete(item.Id);
                    await Navigation.PopAsync();
                })
            });

        }
    }
}
