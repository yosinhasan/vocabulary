using Flurl.Http;
using Newtonsoft.Json;
using Renault.Helpers;
using Renault.Interfaces;
using Renault.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Renault.Services.Managers
{
    /// <summary>
    /// CardManager.
    /// </summary>
    public class CardManager : ICardManager
    {
        public async Task<Card> get(string url)
        {
            Card card = null;
            try
            {
                string response = await url.WithHeaders(new { token = Constants.APP_AUTH_TOKEN,  appname = Constants.APP_AUTH_NAME }).GetStringAsync();
                if (response != "[]") {
                    card = JsonConvert.DeserializeObject<Card>(response);
                }
             
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var toaster = DependencyService.Get<IToastNotifier>();
                    toaster?.Notify(ToastNotificationType.Error, "ERROR OCCURED", ex.Message, TimeSpan.FromSeconds(4f));
                });
            }
            return card;
        }
         
    }

}
