using Cirrious.MvvmCross.ViewModels;

namespace RadixConverter.ViewModel
{
    [PropertyChanged.ImplementPropertyChanged]
    public class RadixConverterViewModel : MvxViewModel
    {
        public string DecimalString { get; set; }

        public string HexString { get; set; }

        public override void Start()
        {
            DecimalString = "0";

            base.Start();
        }

        private void OnDecimalStringChanged()
        {
            int parsedValue;
            if (int.TryParse(DecimalString, out parsedValue))
            {
                HexString = string.Format("{0:X}", parsedValue);
            }
        }

        private void OnHexStringChanged()
        {
            int parsedValue;
            if (int.TryParse(HexString, System.Globalization.NumberStyles.HexNumber, null, out parsedValue))
            {
                DecimalString = string.Format("{0}", parsedValue);
            }
        }
    }
}
    