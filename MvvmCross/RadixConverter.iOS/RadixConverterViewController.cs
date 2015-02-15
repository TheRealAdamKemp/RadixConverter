using System;

using Foundation;
using UIKit;
using Cirrious.MvvmCross.Touch.Views;
using RadixConverter.ViewModel;
using Cirrious.MvvmCross.Binding.BindingContext;

namespace RadixConverter.iOS
{
    public partial class RadixConverterViewController : MvxViewController
    {
        public RadixConverterViewController() : base("RadixConverterViewController", null)
        {
        }

        public new RadixConverterViewModel ViewModel
        {
            get { return (RadixConverterViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.CreateBinding(DecimalStringView).To((RadixConverterViewModel vm) => vm.DecimalString).Apply();
            this.CreateBinding(HexStringView).To((RadixConverterViewModel vm) => vm.HexString).Apply();
        }
    }
}

