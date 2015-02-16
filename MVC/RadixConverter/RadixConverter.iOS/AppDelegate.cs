using Foundation;
using UIKit;

namespace RadixConverter.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        private UIWindow window;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            window = new UIWindow(UIScreen.MainScreen.Bounds) { RootViewController = new RadixConverterViewController() };
            window.MakeKeyAndVisible();
            
            return true;
        }
    }
}

