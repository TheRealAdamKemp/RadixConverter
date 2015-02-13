using System;
using Android.App;
using Cirrious.MvvmCross.Droid.Views;
using RadixConverter.ViewModel;

namespace RadixConverter.Android
{
    [Activity(Label = "Radix Converter", MainLauncher = true)]
    public class RadixConverterActivity : MvxActivity
    {
        public new RadixConverterViewModel ViewModel
        {
            get { return (RadixConverterViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            SetContentView(Resource.Layout.RadixConverter);
        }
    }
}

