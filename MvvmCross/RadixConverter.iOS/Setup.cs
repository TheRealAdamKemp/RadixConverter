using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.Touch.Views.Presenters;

using RadixConverter.ViewModel;

namespace RadixConverter.iOS
{
    public class Setup : MvxTouchSetup
    {
        public Setup (MvxApplicationDelegate appDelegate, IMvxTouchViewPresenter presenter)
            : base(appDelegate, presenter)
        {
        }

        protected override IMvxApplication CreateApp ()
        {
            return new App();
        }
    }
}