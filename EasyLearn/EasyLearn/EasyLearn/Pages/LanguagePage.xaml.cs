using EasyLearn.Helpers;
using EasyLearn.Models;
using EasyLearn.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace EasyLearn.Pages
{
    public partial class LanguagePage : ContentPage
    {
        public LanguagePage()
        {
            InitializeComponent();
        }
        private void ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new EditLanguagePage((Language)e.Item) { Title = Constants.EDIT });
            ((ListView)sender).SelectedItem = null;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await ServiceManager.SqliteService.LanguageManager.readAll();

        }

        public void AddActivated(object sender, EventArgs arg)
        {
            Navigation.PushAsync(new EditLanguagePage() { Title = Constants.ADD_LANGUAGE });
        }
    }
}
