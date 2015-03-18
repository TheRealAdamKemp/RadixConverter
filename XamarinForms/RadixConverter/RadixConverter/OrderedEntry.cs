using Xamarin.Forms;

namespace RadixConverter
{
    [PropertyChanged.ImplementPropertyChanged]
    public class OrderedEntry : Entry
    {
        public Entry NextEntry { get; set; }

        public Entry PreviousEntry { get; set; }
    }
}

