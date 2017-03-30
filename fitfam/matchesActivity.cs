using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace fitfam
{
    [Activity(Label = "matchesActivity")]
    public class matchesActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.matches);

            Button match_button = FindViewById<Button>(Resource.Id.create_event_button);

            find_fam_button.Click += delegate {
                StartActivity(typeof(FindafamformActivity));
            };
        }
    }
}