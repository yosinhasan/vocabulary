using EasyLearn.Models;
using EasyLearn.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace EasyLearn.Pages
{
    public partial class HomePage : ContentPage
    {
        IEnumerable items;
        public HomePage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (ServiceManager.SqliteService.Current == null || ServiceManager.SqliteService.CurrentTranslation == null)
            {
                MainPage.Main.Detail = new NavigationPage(new SettingsPage());
            }
            else
            {
                items = await ServiceManager.SqliteService.WordManager.readAllByCurrentLanguage(ServiceManager.SqliteService.Current.Id, ServiceManager.SqliteService.CurrentTranslation.Id);
                listView.ItemsSource = items;
                Title = ServiceManager.SqliteService.Current.Name.Substring(0, 2).ToUpper() + " - " + ServiceManager.SqliteService.CurrentTranslation.Name.Substring(0, 2).ToUpper();

            }
        }
        private async void SearchButtonClicked(object sender, EventArgs e)
        {
            string search = searchText.Text;
            if (search != null && search.Trim() != "")
            {
                search = search.Trim().ToLower();
                listView.ItemsSource = await ServiceManager.SqliteService.WordManager.searchByKeywordAndLangId(search, ServiceManager.SqliteService.Current.Id, ServiceManager.SqliteService.CurrentTranslation.Id);
            }
        }
        private async void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(e.NewTextValue))
            {
                listView.ItemsSource = await ServiceManager.SqliteService.WordManager.searchByKeywordAndLangId(e.NewTextValue, ServiceManager.SqliteService.Current.Id, ServiceManager.SqliteService.CurrentTranslation.Id);
            }
            else
            {
                listView.ItemsSource = items;
            }
        }

        public void AddActivated(object sender, EventArgs arg)
        {
            Navigation.PushAsync(new WordPage());
        }
        private void ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new DetailWordPage((Word)e.Item) { Title = ((Word)e.Item).Keyword });
            ((ListView)sender).SelectedItem = null;
        }
    }
}
