using System;

using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.App;
using Android.Widget;
using Android.OS;

namespace fitfam
{
    [Activity(Label = "fitfam", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button login_button = FindViewById<Button>(Resource.Id.login_button);
            Button signup_button = FindViewById<Button>(Resource.Id.signup_button);

            login_button.Click += delegate {
                StartActivity(typeof(HomepageActivity));
            };

            signup_button.Click += delegate {
                StartActivity(typeof(HomepageActivity));
            };

            Finish();
        }
    }
}