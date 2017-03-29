using Android.App;
using Android.OS;

namespace fitfam
{
    [Activity(Label = "FamProfileActivity")]
    public class FamProfileActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FamProfile);

            // Create your application here

        }
    }
}