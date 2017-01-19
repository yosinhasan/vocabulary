using Renault.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Renault.Pages
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            masterPage.ListView.ItemSelected += OnItemSelected;
  
        }

    
        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //view.BackgroundColor = Color.FromHex("#ffcc33");
            var item = e.SelectedItem as Category;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.targetType));
                masterPage.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}
