using System;
using Xamarin.Forms;

using RadixConverter.Model;

namespace RadixConverter
{
    public partial class RadixConverterPage : ContentPage
    {
        private RadixConverterModel Model
        {
            get
            {
                return BindingContext as RadixConverterModel;
            }
        }

        public RadixConverterPage()
        {
            InitializeComponent();
        }

        private void HandleEntryEditingCompleted(object sender, EventArgs e)
        {
            if (sender == DecimalStringEntry)
            {
                Model.TrySetDecimalString(DecimalStringEntry.Text);
            }
            else if (sender == HexStringEntry)
            {
                Model.TrySetHexString(HexStringEntry.Text);
            }
        }

        private async void HandleAboutClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage(), animated: true);
        }
    }
}

