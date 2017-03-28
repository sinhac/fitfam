using Android.App;
using Android.OS;
using Android.Widget;

namespace fitfam
{
    [Activity(Label = "CreateafamformActivity")]
    public class CreateafamformActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Createafamform);

            // Create your application here
            Button button2 = FindViewById<Button>(Resource.Id.button2);

            button2.Click += delegate {
                StartActivity(typeof(FamProfileActivity));
            };
        }
    }
}