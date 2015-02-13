using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;

namespace RadixConverter.ViewModel
{
    public class App : MvxApplication
    {
        public App()
        {
            Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<RadixConverterViewModel>());
        }
    }
}