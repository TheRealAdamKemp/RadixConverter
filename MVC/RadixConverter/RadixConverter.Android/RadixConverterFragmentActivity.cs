using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;

using RadixConverter.Model;

namespace RadixConverter.Android
{
//    [Activity(Label = "RadixConverter.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class RadixConverterFragmentActivity : ActivityBase<RadixConverterFragment>
    {
    }

    public class RadixConverterFragment : FragmentBase
    {
        private readonly RadixConverterModel _radixConverterModel = new RadixConverterModel();

        private EditText _decimalStringView;
        private EditText _hexStringView;

        protected override View OnCreateViewCore(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.RadixConverter, container, attachToRoot: false);

            _decimalStringView = FindAndInitializeEditText(view, Resource.Id.DecimalStringView, _radixConverterModel.DecimalString);
            _hexStringView = FindAndInitializeEditText(view, Resource.Id.HexStringView, _radixConverterModel.HexString);

            return view;
        }


        private EditText FindAndInitializeEditText(View view, int id, string text)
        {
            var editText = view.FindViewById<EditText>(id);
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

            InputMethodManager manager = (InputMethodManager) Activity.GetSystemService(Context.InputMethodService);
            manager.HideSoftInputFromWindow(_decimalStringView.WindowToken, 0);
        }
    }
}

