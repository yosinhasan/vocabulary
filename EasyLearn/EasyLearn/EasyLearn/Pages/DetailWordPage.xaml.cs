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
        public DetailWordPage(Word item)
        {
            InitializeComponent();
            this.item = item;

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (this.item != null)
            {
                if (this.item.Text != null)
                {
                    language.Text = Constants.FROM + " " + ServiceManager.SqliteService.Current.Name + " " + Constants.TO + " " + ServiceManager.SqliteService.CurrentTranslation.Name;
                    word.Text = item.Keyword;
                    transcript.Text = item.Transript;
                    translationWord.Text = item.Text;
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
        public void SaveActivated(object sender, EventArgs e)
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
                        item.Transript = (translationTranscript != null) ? translationTranscript.Trim().ToLower() : "";
                        item.Text = translation.Trim().ToLower();
                        item.Keyword = keyword;
                        await ServiceManager.SqliteService.WordManager.update(item);
                        this.item = null;
                        await Navigation.PopAsync();
                    }

                })
            });
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
                    if (!result) return;
                    await ServiceManager.SqliteService.WordManager.delete(item);
                    await Navigation.PopAsync();
                })
            });

        }
    }
}
