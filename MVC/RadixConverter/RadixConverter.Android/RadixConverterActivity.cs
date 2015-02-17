using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views.InputMethods;

using RadixConverter.Model;

namespace RadixConverter.Android
{
    [Activity(Label = "RadixConverter.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class RadixConverterActivity : Activity
    {
        private readonly RadixConverterModel _radixConverterModel = new RadixConverterModel();

        private EditText _decimalStringView;
        private EditText _hexStringView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.RadixConverter);

            _decimalStringView = FindAndInitializeEditText(Resource.Id.DecimalStringView, _radixConverterModel.DecimalString);
            _hexStringView = FindAndInitializeEditText(Resource.Id.HexStringView, _radixConverterModel.HexString);
        }

        private EditText FindAndInitializeEditText(int id, string text)
        {
            var editText = FindViewById<EditText>(id);
            editText.Text = text;
            editText.EditorAction += HandleEditorAction;
            return editText;
        }

        private void HandleEditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            if (sender == _decimalStringView)
            {
                _radixConverterModel.TrySetDecimalString(_decimalStringView.Text);
            }
            else if (sender == _hexStringView)
            {
                _radixConverterModel.TrySetHexString(_hexStringView.Text);
            }

            _decimalStringView.Text = _radixConverterModel.DecimalString;
            _hexStringView.Text = _radixConverterModel.HexString;

            InputMethodManager manager = (InputMethodManager) GetSystemService(InputMethodService);
            manager.HideSoftInputFromWindow(_decimalStringView.WindowToken, 0);
        }
    }
}


