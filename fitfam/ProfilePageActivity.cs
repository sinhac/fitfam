using Android.App;
using Android.OS;
using Android.Widget;

namespace fitfam
{
    [Activity(Label = "ProfilePageActivity")]
    public class ProfilePageActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.UserProfilePage);

            // Create your application here
            Button myfams_button = FindViewById<Button>(Resource.Id.myFamButton);
            myfams_button.Click += delegate {
                StartActivity(typeof(FamProfileActivity));
            };

            Button editprofile_button = FindViewById<Button>(Resource.Id.editProfileButton);
            editprofile_button.Click += delegate {
                StartActivity(typeof(EditProfilePageActivity));
            };
        }
    }
}