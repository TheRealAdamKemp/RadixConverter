using System;
using System.Drawing;

using Foundation;
using UIKit;

namespace RadixConverter.iOS
{
    public partial class RadixConverter_iOSViewController : UIViewController
    {
        static bool UserInterfaceIdiomIsPhone
        {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }

        public RadixConverter_iOSViewController(IntPtr handle)
            : base(handle)
        {
        }
    }
}

