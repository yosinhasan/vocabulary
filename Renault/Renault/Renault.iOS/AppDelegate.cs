using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace Renault.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes()
            {
                TextColor = UIColor.Black
            });
            //UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(38, 176, 90);green
            //            UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(25, 118, 210);blue
            UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(255, 255, 255);

            UINavigationBar.Appearance.TintColor = UIColor.Black;
            UIBarButtonItem.Appearance.TintColor = UIColor.Black;


            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
