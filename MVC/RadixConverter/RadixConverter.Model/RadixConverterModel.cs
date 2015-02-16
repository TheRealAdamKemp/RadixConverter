using System.Globalization;

namespace RadixConverter.Model
{
    [PropertyChanged.ImplementPropertyChanged]
    public class RadixConverterModel
    {
        #region Private Constants

        private const NumberStyles DecimalNumberStyles = NumberStyles.Integer;
        private const NumberStyles HexNumberStyles = NumberStyles.HexNumber;

        private const string DecimalFormat = "{0}";
        private const string HexFormat = "{0:X}";

        #endregion

        public int Value { get; set; }

        public string DecimalString { get; private set; }

        public string HexString { get; private set; }

        public bool TrySetDecimalString(string decimalString)
        {
            return TrySetStringCore(decimalString, DecimalNumberStyles);
        }

        public bool TrySetHexString(string hexString)
        {
            return TrySetStringCore(hexString, HexNumberStyles);
        }

        #region Private Methods

        private void OnValueChanged()
        {
            DecimalString = FormatValue(DecimalFormat);
            HexString = FormatValue(HexFormat);
        }

        private bool TrySetStringCore(string valueString, NumberStyles numberStyles)
        {
            int parsedValue;
            bool success = int.TryParse(valueString, numberStyles, null, out parsedValue);
            if (success)
            {
                Value = parsedValue;
            }

            return success;
        }

        private string FormatValue(string format)
        {
            return string.Format(format, Value);
        }

        #endregion
    }
}

