using System;
using System.Threading.Tasks;

using Android.App;
using Android.OS;
using Android.Runtime;

namespace RadixConverter.Android
{
    public class ApplicationBase : Application
    {
        public ApplicationBase(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
            if (Instance != null)
            {
                throw new InvalidOperationException("Cannot create multiple instances of Application");
            }

            Instance = this;
            AppDomain.CurrentDomain.UnhandledException += HandleUnhandledException;
        }

        /// <summary>
        /// True if the app is fully initialized. Only check this from the UI thread.
        /// </summary>
        public bool IsInitialized { get; private set; }

        /// <summary>
        /// The singleton instance of this application.
        /// </summary>
        public static ApplicationBase Instance { get; private set; }

        /// <summary>
        /// Called from the UI thread when the app has finished initializing.
        /// </summary>
        public event EventHandler Initialized;

        /// <summary>
        /// Called before any activity is created. If this override does not exist then our constructor is not called until something
        /// tries to use it. Therefore most initialization code should really go here instead of the constructor.
        /// </summary>
        public override void OnCreate()
        {
            base.OnCreate();

            Task.Factory.StartNew(() =>
                {
                    InitializeInBackground();

                    new Handler(Looper.MainLooper).Post(() =>
                        {
                            IsInitialized = true;
                            OnInitialized();
                        });
                });
        }

        /// <summary>
        /// Does any initialization work that might take a long time in the background.
        /// </summary>
        protected virtual void InitializeInBackground()
        {
        }

        private void OnInitialized()
        {
            if (Initialized != null)
            {
                Initialized(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Log unhandled exceptions before crashing. Based of a pattern in Xamarin's "Advanced Android App Lifecycle" session at Evolve.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void HandleUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            Console.WriteLine("<<<Unhandled {1} Exception: {0}", ex.Message, e.IsTerminating ? "Terminating" : "Non-Terminating");
            Console.WriteLine("{0}>>>", ex.StackTrace);
        }
    }
}
