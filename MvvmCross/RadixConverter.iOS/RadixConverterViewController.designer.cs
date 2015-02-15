// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace RadixConverter.iOS
{
	[Register ("RadixConverterViewController")]
	partial class RadixConverterViewController
	{
		[Outlet]
		UIKit.UITextField DecimalStringView { get; set; }

		[Outlet]
		UIKit.UITextField HexStringView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (DecimalStringView != null) {
				DecimalStringView.Dispose ();
				DecimalStringView = null;
			}

			if (HexStringView != null) {
				HexStringView.Dispose ();
				HexStringView = null;
			}
		}
	}
}
