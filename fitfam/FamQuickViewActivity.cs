using Android.App;
using Android.OS;

namespace fitfam
{
    [Activity(Label = "FamQuickViewActivity")]
    public class FamQuickViewActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            Button create_event_button = FindViewById<Button>(Resource.Id.create_event_button);

            find_fam_button.Click += delegate {
                StartActivity(typeof(FindafamformActivity));
            };
        }
    }
}