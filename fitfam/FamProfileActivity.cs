using Android.App;
using Android.OS;

namespace fitfam
{
    [Activity(Label = "Activity1")]
    public class FamProfileActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.FamProfile);
        }
    }
}