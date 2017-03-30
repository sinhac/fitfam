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
    [Activity(Label = "CreateActivityActivity")]
    public class CreateEventActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Event newEvent = new Event();

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CreateEventPage);

            // Create your application here
            Button button2 = FindViewById<Button>(Resource.Id.button2);

            button2.Click += delegate {
                StartActivity(typeof(matchesActivity));
            };
            Finish();
        }
    }
}