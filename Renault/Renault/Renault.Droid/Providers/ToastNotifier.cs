using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using System.Threading.Tasks;
using Renault.Interfaces;
[assembly: Dependency(typeof(Renault.Droid.Providers.ToastNotifier))]
namespace Renault.Droid.Providers
{
    public class ToastNotifier : IToastNotifier
    {
        public Task<bool> Notify(ToastNotificationType type, string title, string description, TimeSpan duration, object context = null)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();
            Toast.MakeText(Forms.Context, description, ToastLength.Short).Show();
            return taskCompletionSource.Task;
        }

        public void HideAll()
        {
        }
    }
}