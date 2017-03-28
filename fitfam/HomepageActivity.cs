using Android.App;
using Android.OS;
using Android.Widget;

namespace fitfam
{
    [Activity(Label = "HomepageActivity")]
    public class HomepageActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Homepage);

            // Create your application here
            Button find_fam_button = FindViewById<Button>(Resource.Id.find_fam_button);
            Button create_fam_button = FindViewById<Button>(Resource.Id.create_fam_button);
            Button find_event_button = FindViewById<Button>(Resource.Id.find_event_button);
            Button create_event_button = FindViewById<Button>(Resource.Id.create_event_button);

            find_fam_button.Click += delegate {
                StartActivity(typeof(FindafamformActivity));
            };

            create_fam_button.Click += delegate {
                StartActivity(typeof(CreateafamformActivity));
            };

            find_event_button.Click += delegate {
                StartActivity(typeof(FindaneventActivity));
            };

            create_event_button.Click += delegate {
                StartActivity(typeof(CreateEventActivity));
            };
        }
    }
}