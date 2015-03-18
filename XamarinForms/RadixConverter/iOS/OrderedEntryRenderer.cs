using System;
using System.ComponentModel;
using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using RadixConverter;

[assembly: ExportRenderer(typeof(OrderedEntry), typeof(RadixConverter.iOS.OrderedEntryRenderer))]

namespace RadixConverter.iOS
{
    public class OrderedEntryRenderer : EntryRenderer
    {
        private UIToolbar _inputAccessoryView;
        private UIBarButtonItem _previousBarButton;
        private UIBarButtonItem _nextBarButton;

        private OrderedEntry OrderedEntry { get { return (OrderedEntry)Element; } }

        private EntryRenderer PreviousEntryRenderer { get { return GetRelativeEntryRenderer(OrderedEntry.PreviousEntry); } }

        private EntryRenderer NextEntryRenderer { get { return GetRelativeEntryRenderer(OrderedEntry.NextEntry); } }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null && Control.InputAccessoryView == null)
            {
                CreateInputAccessoryView();
            }

            if (e.OldElement != null)
            {
                e.OldElement.Focused -= HandleFocused;
            }
            if (e.NewElement != null)
            {
                e.NewElement.Focused += HandleFocused;
            }
        }

        private void CreateInputAccessoryView()
        {
            _previousBarButton = CreateBarButtonItem("<");
            _nextBarButton = CreateBarButtonItem(">");
            _inputAccessoryView = new UIToolbar() { Items = new[] { _previousBarButton, _nextBarButton } };
            _inputAccessoryView.SizeToFit();

            Control.InputAccessoryView = _inputAccessoryView;

            UpdateToolbarItems();
        }

        private UIBarButtonItem CreateBarButtonItem(string label)
        {
            return new UIBarButtonItem(label, UIBarButtonItemStyle.Plain, HandleToolbarButtonPressed);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == "NextEntry" || e.PropertyName == "PreviousEntry")
            {
                UpdateToolbarItems();
            }
        }

        private void UpdateToolbarItems()
        {
            _previousBarButton.Enabled = PreviousEntryRenderer != null;
            _nextBarButton.Enabled = NextEntryRenderer != null;
        }

        private void HandleFocused(object sender, FocusEventArgs e)
        {
            UpdateToolbarItems();
        }

        private void HandleToolbarButtonPressed(object sender, EventArgs e)
        {
            EntryRenderer itemToFocus = null;
            if (sender == _previousBarButton)
            {
                itemToFocus = PreviousEntryRenderer;
            }
            else if (sender == _nextBarButton)
            {
                itemToFocus = NextEntryRenderer;
            }

            if (itemToFocus != null)
            {
                itemToFocus.Control.BecomeFirstResponder();
            }
        }

        EntryRenderer GetRelativeEntryRenderer(Entry entry)
        {
            return GetRenderer(entry) as EntryRenderer;
        }

        #region GetRenderer Hack

        private delegate IVisualElementRenderer GetRendererDelegate(BindableObject bindable);

        private static GetRendererDelegate _getRendererDelegate;

        private static IVisualElementRenderer GetRenderer(BindableObject bindable)
        {
            if (bindable == null)
            {
                return null;
            }

            if (_getRendererDelegate == null)
            {
                var assembly = typeof(EntryRenderer).Assembly;
                var platformType = assembly.GetType("Xamarin.Forms.Platform.iOS.Platform");
                var method = platformType.GetMethod("GetRenderer");
                _getRendererDelegate = (GetRendererDelegate)method.CreateDelegate(typeof(GetRendererDelegate));
            }

            return _getRendererDelegate(bindable);
        }

        #endregion
    }
}

