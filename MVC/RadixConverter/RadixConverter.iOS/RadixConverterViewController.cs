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
            
            DecimalStringView.ShouldReturn += HandleShouldReturn;
            HexStringView.ShouldReturn += HandleShouldReturn;
        }

        private bool HandleShouldReturn(UITextField sender)
        {
            if (sender == DecimalStringView)
            {
                if (_radixConverterModel.TrySetDecimalString(DecimalStringView.Text))
                {
                    HexStringView.Text = _radixConverterModel.HexString;
                }
            }
            else if (sender == HexStringView)
            {
                if (_radixConverterModel.TrySetHexString(HexStringView.Text))
                {
                    DecimalStringView.Text = _radixConverterModel.DecimalString;
                }
            }

            View.EndEditing(true);

            return true;
        }
    }
}

