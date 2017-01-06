using EasyLearn.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using FormsToolkit;
using EasyLearn.Helpers;

namespace EasyLearn
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            SubscribeToDisplayAlertMessages();

            Task<bool> cond;
            cond = Task.Run(async () =>
            {
                return await LocalService.SqliteService.SetUp();
            });

            MainPage = new Pages.MainPage(cond.Result);
        }
        /// <summary>
        /// Subscribes to messages for displaying alerts.
        /// </summary>
        private static void SubscribeToDisplayAlertMessages()
        {
            MessagingService.Current.Subscribe<MessagingServiceAlert>(MessageKeys.DisplayAlert, async (service, info) => {
                var task = Current?.MainPage?.DisplayAlert(info.Title, info.Message, info.Cancel);
                if (task != null)
                {
                    await task;
                    info?.OnCompleted?.Invoke();
                }
            });

            MessagingService.Current.Subscribe<MessagingServiceQuestion>(MessageKeys.DisplayQuestion, async (service, info) => {
                var task = Current?.MainPage?.DisplayAlert(info.Title, info.Question, info.Positive, info.Negative);
                if (task != null)
                {
                    var result = await task;
                    info?.OnCompleted?.Invoke(result);
                }
            });
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
