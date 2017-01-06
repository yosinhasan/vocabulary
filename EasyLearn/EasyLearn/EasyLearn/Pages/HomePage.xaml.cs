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
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (LocalService.SqliteService.Current == null || LocalService.SqliteService.CurrentTranslation == null)
            {
                MainPage.Main.Detail = new NavigationPage(new SettingsPage());
            } else
            {
                listView.ItemsSource = await LocalService.SqliteService.WordManager.readAllByCurrentLanguage(LocalService.SqliteService.Current.Id, LocalService.SqliteService.CurrentTranslation.Id);
                Title = LocalService.SqliteService.Current.Name.Substring(0, 2).ToUpper() + " - " + LocalService.SqliteService.CurrentTranslation.Name.Substring(0, 2).ToUpper();
            }
        }
        private async void SearchButtonClicked(object sender, EventArgs e)
        {
            string search = searchText.Text;
            if (search != null && search.Trim() != "")
            {
                search = search.Trim().ToLower();
                listView.ItemsSource = await LocalService.SqliteService.WordManager.searchByKeywordAndLangId(search, LocalService.SqliteService.Current.Id,  LocalService.SqliteService.CurrentTranslation.Id);
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
