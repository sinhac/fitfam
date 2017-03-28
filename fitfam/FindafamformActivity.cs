using Android.App;
using Android.OS;
using Android.Widget;

namespace fitfam
{
    [Activity(Label = "FindafamformActivity")]
    public class FindafamformActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Findafamform);

            // Create your application here
            Button button2 = FindViewById<Button>(Resource.Id.button2);

            button2.Click += delegate {
                StartActivity(typeof(matchesActivity));
            };
        }
    }
}