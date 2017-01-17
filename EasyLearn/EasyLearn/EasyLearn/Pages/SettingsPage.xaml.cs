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
            var menu = new List<Category>();
            menu.Add(new Category
            {
                Label = "Choose mode",
                Image = "switch2.png",
                TargetType = typeof(ChooseModePage)
            });
            menu.Add(new Category
            {
                Label = "Languages",
                Image = "lang.png",
                TargetType = typeof(LanguagePage)
            });

            menu.Add(new Category
            {
                Label = "Export data",
                Image = "export.png",
                TargetType = typeof(ExportPage)
            });
            menu.Add(new Category
            {
                Label = "Import data",
                Image = "import2.png",
                TargetType = typeof(ImportPage)
            });
            listView.ItemsSource = menu;
            listView.ItemSelected += OnItemSelected;
        }
        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //view.BackgroundColor = Color.FromHex("#ffcc33");
            var item = e.SelectedItem as Category;
            if (item != null)
            {
                await Navigation.PushAsync((Page)Activator.CreateInstance(item.TargetType));
                listView.SelectedItem = null;
            }
        }
    }
}
