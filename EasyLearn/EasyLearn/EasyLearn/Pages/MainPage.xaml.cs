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
    public partial class MainPage : MasterDetailPage
    {
        private static MainPage _instance;
        public static MainPage Main
        {
            get
            {
                return _instance;
            }
            private set
            {

            }
        }

        public MainPage()
        {
            InitializeComponent();
            masterPage.ListView.ItemSelected += OnItemSelected;
            _instance = this;
            Detail = new NavigationPage(new HomePage());
        }

        public MainPage(bool cond)
        {
            InitializeComponent();
            masterPage.ListView.ItemSelected += OnItemSelected;
            _instance = this;
            if (cond)
            {
                Detail = new NavigationPage(new HomePage());
            }
            else
            {
                Detail = new NavigationPage(new SettingsPage());

            }
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //view.BackgroundColor = Color.FromHex("#ffcc33");
            var item = e.SelectedItem as Category;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                masterPage.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}
