using UIKit;

using RadixConverter.Model;

namespace RadixConverter.iOS
{
    public partial class RadixConverterViewController : UIViewController
    {
        private readonly RadixConverterModel _radixConverterModel = new RadixConverterModel();

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitializeStringView(DecimalStringView, _radixConverterModel.DecimalString);
            InitializeStringView(HexStringView, _radixConverterModel.HexString);
        }

        private void InitializeStringView(UITextField view, string text)
        {
            view.ShouldReturn += HandleShouldReturn;
            view.Text = text;
        }

        private bool HandleShouldReturn(UITextField sender)
        {
            if (sender == DecimalStringView)
            {
                _radixConverterModel.TrySetDecimalString(DecimalStringView.Text);
            }
            else if (sender == HexStringView)
            {
                _radixConverterModel.TrySetHexString(HexStringView.Text);
            }

            DecimalStringView.Text = _radixConverterModel.DecimalString;
            HexStringView.Text = _radixConverterModel.HexString;

            View.EndEditing(true);

            return true;
        }
    }
}

