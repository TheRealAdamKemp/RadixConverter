using Foundation;
using UIKit;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.Touch.Views.Presenters;
using Cirrious.MvvmCross.ViewModels;

namespace RadixConverter.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : MvxApplicationDelegate
    {
        // class-level declarations
        
        public override UIWindow Window
        {
            get;
            set;
        }
        
        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            Window = new UIWindow(UIScreen.MainScreen.Bounds);
            var presenter = new CustomPresenter(this, Window);
            var setup = new Setup(this, presenter);
            setup.Initialize();
            var startup = Mvx.Resolve<IMvxAppStart>();
            startup.Start();

            Window.MakeKeyAndVisible();
            return true;
        }

        private class CustomPresenter : MvxTouchViewPresenter
        {
            public CustomPresenter(UIApplicationDelegate applicationDelegate, UIWindow window) : base(applicationDelegate, window)
            {
            }

            protected override UINavigationController CreateNavigationController(UIViewController viewController)
            {
                var navController = base.CreateNavigationController(viewController);
                navController.NavigationBarHidden = true;

                return navController;
            }
        }
    }
}

