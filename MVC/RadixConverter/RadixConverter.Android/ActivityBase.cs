using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;

namespace RadixConverter.Android
{
    /// <summary>
    /// Common base class for Activity implementations.
    /// <remarks>
    /// It is imperative that all Activity classes derive from
    /// this rather than directly from Activity.  This class
    /// keeps a cache of the currently running Activity for
    /// low level code that does not have access to that information.
    /// </remarks>
    /// </summary>
    public class ActivityBase : Activity
    {
        private static object _currentActivityLock = new object ();
        private static ActivityBase _currentActivity = null;

        /// <summary>
        /// Gets the currently active Activity for use by lower level elements
        /// that don't necessarily have access to this information from deep in
        /// the infrastructure code.
        /// </summary>
        /// <remarks>
        /// Because this class maintains the value of this property
        /// it is very important that all Activities derive from ActivityBase
        /// In addition, it is imperative to never access this property from
        /// outside the UI thread otherwise it could be changed out from under
        /// you.
        /// </remarks>
        /// <value>
        /// The currently running Activity or null if there isn't one.
        /// </value>
        public static ActivityBase CurrentActivity
        {
            get 
            {
                lock(_currentActivityLock)
                {
                    return _currentActivity;
                }
            }

            private set
            {
                lock(_currentActivityLock)
                {
                    _currentActivity = value;
                }
            }
        }

        /// <summary>
        /// Gets the current context (the currently active Activity)
        /// for use by lower level elements that don't necessarily 
        /// have access to this information from deep in the infrastructure
        /// code.
        /// </summary>
        /// <remarks>
        /// Because this class maintains the value of this property
        /// it is very important that all Activities derive from ActivityBase
        /// In addition, it is imperative to never access this property from
        /// outside the UI thread otherwise it could be changed out from under
        /// you.
        /// </remarks>
        /// <value>
        /// The currently running Activity or the Application's Context
        /// if there isn't one
        /// </value>
        public static Context CurrentContext
        {
            get
            {
                return CurrentActivity ?? Application.Context;
            }
        }

        /// <inheritdoc />
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            CurrentActivity = this;
        }

        /// <inheritdoc />
        protected override void OnDestroy()
        {
            // Xamarin.Android's GC runs very rarely, but the managed objects can hold Java objects in memory.
            // When an activity is destroyed it is likely that there are a bunch of views associated with it which
            // could also be destroyed if only the GC would bother to collect them. Therefore we are encouraging the
            // GC to run after destroying the activity.
            // We post this in order to be sure that the activity is no longer in use, and therefore the activity itself
            // is a candidate for collection as well.
            new Handler(Looper.MainLooper).Post(GC.Collect);

            base.OnDestroy();
        }

        /// <inheritdoc />
        protected override void OnPause()
        {
            base.OnPause ();
            CurrentActivity = null;
        }

        /// <inheritdoc />
        protected override void OnResume()
        {
            base.OnResume ();
            CurrentActivity = this;
        }
    }

    public class ActivityBase<TFragment> : ActivityBase where TFragment : FragmentBase, new()
    {
        /// <summary>
        /// The top-level fragment which manages the view and state for this activity.
        /// </summary>
        public FragmentBase Fragment { get; protected set; }

        /// <summary>
        /// The tag string to use when finding or creating this activity's fragment. This will be contructed using the type of this generic instance.
        /// </summary>
        protected string FragmentTag
        {
            get
            {
                return GetType().Name;
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Window.RequestFeature(WindowFeatures.ActionBar);
            if (!ApplicationBase.Instance.IsInitialized)
            {
                // The app is not yet initialized. Splash screen is already showing, but hide the action bar.
                PreInitialization();
            }
            else
            {
                // The app is already initialized. Do the UI initialization (pre-drop controls) if necessary.
                FinishInitialization();
            }
        }

        /// <inheritdoc />
        protected override void OnResume()
        {
            base.OnResume();

            if (!ApplicationBase.Instance.IsInitialized)
            {
                ApplicationBase.Instance.Initialized += OnAppInitialized;
            }
            else
            {
                FinishInitialization();
            }
        }

        /// <inheritdoc />
        public override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
            if (Fragment != null)
            {
                Fragment.OnAttachedToWindow();
            }
        }

        /// <inheritdoc />
        protected override void OnPause()
        {
            base.OnPause();

            if (!ApplicationBase.Instance.IsInitialized)
            {
                // The app is still not initialized which means our handler is still attached.
                // Since this activity seems to be leaving we will unattach and let some other activity instance handle it instead.
                ApplicationBase.Instance.Initialized -= OnAppInitialized;
            }
        }

        protected override void OnNewIntent(Intent intent)
        {
            Fragment.OnNewIntent(intent);
        }

        /// <summary>
        /// Loads the fragment for this activity and stores it in the Fragment property.
        /// </summary>
        protected virtual void LoadFragment()
        {
            Fragment = FragmentBase.FindOrCreateFragment<TFragment>(this, FragmentTag, global::Android.Resource.Id.Content);
        }

        /// <summary>
        /// The event handler for when the app is initialized.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="args">The event args.</param>
        private void OnAppInitialized(object sender, EventArgs args)
        {
            ApplicationBase.Instance.Initialized -= OnAppInitialized;
            FinishInitialization();
        }

        /// <summary>
        /// Called when this activity is created, but our app has not yet initialized
        /// i.e. - a splash screen is still showing.  Override to do things
        /// like hide the action bar.
        /// </summary>
        protected virtual void PreInitialization()
        {
        }

        /// <summary>
        /// Loads the real content view for this activity and puts it on the screen.
        /// </summary>
        protected virtual void FinishInitialization()
        {
            OnUIInitializationComplete();
        }

        private void OnUIInitializationComplete()
        {
            if (Fragment == null)
            {
                LoadFragment();
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            Fragment.OnActivityResult(requestCode, resultCode, data);
        }
    }
}
