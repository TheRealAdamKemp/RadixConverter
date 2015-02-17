using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace RadixConverter.Android
{
    /// <summary>
    /// The base class for top-level fragments in Android. These are the fragments which maintain the view hierarchy and state for each top-level
    /// Activity. These fragments all use RetainInstance = true to allow them to maintain state across configuration changes (i.e.,
    /// when the device rotates we reuse the fragments). Activity classes are basically just dumb containers for these fragments.
    /// </summary>
    public abstract class FragmentBase : Fragment
    {
        private FrameLayout _rootView;

        /// <summary>
        /// Tries to locate an already created fragment with the given tag. If the fragment is not found then a new one will be created and inserted into
        /// the given activity using the given containerId as the parent view.
        /// </summary>
        /// <typeparam name="TFragment">The type of fragment to create.</typeparam>
        /// <param name="activity">The activity to search for or create the view in.</param>
        /// <param name="fragmentTag">The tag which uniquely identifies the fragment.</param>
        /// <param name="containerId">The resource ID of the parent view to use for a newly created fragment.</param>
        /// <returns></returns>
        public static TFragment FindOrCreateFragment<TFragment>(Activity activity, string fragmentTag, int containerId) where TFragment : FragmentBase, new()
        {
            var fragment = activity.FragmentManager.FindFragmentByTag(fragmentTag) as TFragment;
            if (fragment == null)
            {
                fragment = new TFragment();
                activity.FragmentManager.BeginTransaction().Add(containerId, fragment, fragmentTag).Commit();
            }

            return fragment;
        }

        /// <summary>
        /// This is just a more type-specific version of the base class's Activity property.
        /// </summary>
        public new ActivityBase Activity
        {
            get
            {
                return (ActivityBase)base.Activity;
            }
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            RetainInstance = true;
        }

        public sealed override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _rootView = new FrameLayout(Activity);

            var containedView = OnCreateViewCore(inflater, container, savedInstanceState);
            containedView.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            _rootView.AddView(containedView);

            return _rootView;
        }

        protected abstract View OnCreateViewCore(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState);

        public override void OnDestroyView()
        {
            base.OnDestroyView();

            _rootView = null;
        }

        public virtual void OnNewIntent(Intent intent)
        {
        }

        /// <summary>
        /// Base method does nothing.
        /// </summary>
        public virtual void OnAttachedToWindow()
        {
        }
    }
}