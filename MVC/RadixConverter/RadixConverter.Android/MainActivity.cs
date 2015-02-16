using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views.InputMethods;

using RadixConverter.Model;

namespace RadixConverter.Android
{
    [Activity(Label = "RadixConverter.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private readonly RadixConverterModel _radixConverterModel = new RadixConverterModel();

        private EditText _decimalStringView;
        private EditText _hexStringView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            _decimalStringView = FindViewById<EditText>(Resource.Id.DecimalStringView);
            _hexStringView = FindViewById<EditText>(Resource.Id.HexStringView);
            
            _decimalStringView.EditorAction += HandleEditorAction;
            _hexStringView.EditorAction += HandleEditorAction;
        }

        private void HandleEditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            if (sender == _decimalStringView)
            {
                if (_radixConverterModel.TrySetDecimalString(_decimalStringView.Text))
                {
                    _hexStringView.Text = _radixConverterModel.HexString;
                }
            }
            else if (sender == _hexStringView)
            {
                if (_radixConverterModel.TrySetHexString(_hexStringView.Text))
                {
                    _decimalStringView.Text = _radixConverterModel.DecimalString;
                }
            }

            InputMethodManager manager = (InputMethodManager) GetSystemService(InputMethodService);
            manager.HideSoftInputFromWindow(_decimalStringView.WindowToken, 0);
        }
    }
}


