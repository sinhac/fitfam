using Android.App;
using Android.OS;

namespace fitfam
{
    [Activity(Label = "ProfilePageActivity")]
    public class ProfilePageActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ProfilePage);

            // Create your application here
        }
    }
}