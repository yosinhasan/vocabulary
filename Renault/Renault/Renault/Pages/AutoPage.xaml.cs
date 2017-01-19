using Renault.Helpers;
using Renault.Interfaces;
using Renault.Models;
using Renault.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Renault.Pages
{
    public partial class AutoPage : ContentPage
    {
        public AutoPage()
        {
            InitializeComponent();
        }
        private async void AutoSearchButtonClicked(object sender, EventArgs args)
        {
            butLogin.IsEnabled = false;
            string license = code.Text;
            if (license != null && license.Trim() != "")
            {
                Card card = await ServiceManager.Manager.get(license);
                if (card != null)
                {
                    butLogin.IsEnabled = true;
                    var navPage = new MainPage(); 
                    await Navigation.PushModalAsync(navPage);
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        var toaster = DependencyService.Get<IToastNotifier>();
                        toaster?.Notify(ToastNotificationType.Error, "FOUT OPGETREDEN", Messages.CARD_NOT_FOUND, TimeSpan.FromSeconds(4f));
                    });
                }
            }
            else
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var toaster = DependencyService.Get<IToastNotifier>();
                    toaster?.Notify(ToastNotificationType.Error, "FOUT OPGETREDEN", Messages.FIELD_EMPTY, TimeSpan.FromSeconds(4f));
                });
            }
            butLogin.IsEnabled = true;

        }
    }
}
