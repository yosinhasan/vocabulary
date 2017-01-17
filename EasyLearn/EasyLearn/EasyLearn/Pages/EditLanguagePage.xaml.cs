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
    public partial class EditLanguagePage : ContentPage
    {
        private Language item;

        public EditLanguagePage()
        {
            InitializeComponent();
            formText.Text = Constants.ADD;
            ToolbarItems.Remove(deleteButton);
        }

        public EditLanguagePage(Language item)
        {
            InitializeComponent();
            this.item = item;
            formText.Text = Constants.EDIT;
            formLang.Text = this.item.Name;
        }

        public void SaveActivated(object sender, EventArgs e)
        {
            string text = formLang.Text;
            if (text == null || text.Trim() == "")
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
                MessagingService.Current.SendMessage<MessagingServiceQuestion>(MessageKeys.DisplayQuestion, new MessagingServiceQuestion()
                {
                    Title = Messages.SAVE_DATA,
                    Question = null,
                    Positive = Constants.YES,
                    Negative = Constants.NO,
                    OnCompleted = new Action<bool>(async result =>
                    {
                        if (!result) return;
                        string str = text.Trim().ToLower();
                        char c = (char)(str[0] - 32);
                        string newValue = c.ToString() + str.Substring(1);
                        if (item == null)
                        {
                            await ServiceManager.SqliteService.LanguageManager.create(new Language
                            {
                                Name = newValue
                            });
                        }
                        else if (!item.Name.Equals(newValue))
                        {
                            item.Name = newValue;
                            await ServiceManager.SqliteService.LanguageManager.update(item);
                        }
                        await Navigation.PopAsync();
                    })
                });
            }
        }
        public void DeleteActivated(object sender, EventArgs e)
        {
            if (item.Current == 1 || item.CurrentTranslation == 1)
            {
                MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.DisplayAlert, new MessagingServiceAlert()
                {
                    Title = Titles.ERROR,
                    Message = Messages.LANGUAGE_IS_USED,
                    Cancel = Titles.CANCEL
                });
            }
            else
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
                        await ServiceManager.SqliteService.WordManager.deleteByLangId(item);
                        await ServiceManager.SqliteService.LanguageManager.delete(item);
                        await Navigation.PopAsync();
                    })
                });
            }

        }
    }
}
