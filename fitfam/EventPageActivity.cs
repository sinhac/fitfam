using Android.App;
using Android.OS;

namespace fitfam
{
    [Activity(Label = "EventPage")]
    public class EventPageActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EventPage);

            // Create your application here
        }
    }
}