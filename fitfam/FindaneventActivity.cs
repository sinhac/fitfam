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
    [Activity(Label = "FindaneventActivity")]
    public class FindaneventActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.findaneventform);

            // Create your application here
            Button button2 = FindViewById<Button>(Resource.Id.button2);

            button2.Click += delegate {
                StartActivity(typeof(matchesActivity));
            };
        }
    }
}