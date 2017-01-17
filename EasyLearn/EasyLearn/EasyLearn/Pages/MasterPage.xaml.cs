using EasyLearn.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace EasyLearn.Pages
{
    public partial class MasterPage : ContentPage
    {
        public ListView ListView { get { return listView; } }
        public MasterPage()
        {
            InitializeComponent();
            var menu = new List<Category>();
            menu.Add(new Category
            {
                Label = "Vocabulary",
                Image = "library.png",
                TargetType = typeof(HomePage)
            });
            menu.Add(new Category
            {
                Label = "Settings",
                Image = "settings.png",
                TargetType = typeof(SettingsPage)
            });


            Device.BeginInvokeOnMainThread(() =>
            {
                listView.ItemsSource = menu;
            });
        }
    }
}
