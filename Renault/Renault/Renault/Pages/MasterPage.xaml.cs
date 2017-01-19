using Renault.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Renault.Pages
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
                label = "Auto zoeken",
                image = "auto.png",
                targetType = typeof(AutoPage)
            });
            menu.Add(new Category
            {
                label = "Pas",
                image= "pas.png",
                targetType = typeof(PasPage)
            });
            menu.Add(new Category
            {
                label = "Uw dealer",
                image ="dealer.png",
                targetType = typeof(DealerPage)
            });
            menu.Add(new Category
            {
                label = "De 7 zekerheden",
                image = "info.png",
                targetType = typeof(InfoPage)
            });

            Device.BeginInvokeOnMainThread(() =>
            {
                listView.ItemsSource = menu;
            });
        }
    }
}
