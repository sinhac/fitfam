using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Gms.Common.Apis;
using Android.Gms.Common;
using Android.Gms.Plus;
using Android.Gms.Plus.Model.People;

namespace fitfam
{
    [Activity(Label = "ProfilePageActivity")]
    public class ProfilePageActivity : Activity
    {
        GoogleApiClient mGoogleApiClient;
        private SignInButton mGoogleSignOut;
        private bool mIntentInProgress;
        private bool mSignOutClicked;
        private bool mInfoPopulated;
        private ConnectionResult mConnectionResult;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.UserProfilePage);

            // Create your application here
            Button myfams_button = FindViewById<Button>(Resource.Id.myFamButton);
            myfams_button.Click += delegate
            {
                StartActivity(typeof(FamProfileActivity));
            };

            Button editprofile_button = FindViewById<Button>(Resource.Id.editProfileButton);
            editprofile_button.Click += delegate
            {
                StartActivity(typeof(EditProfilePageActivity));
            };


            if (true/*EVENTUALLY INPUT DATA REGARDING OWNERSHIP/ADMIN*/)
            {
                GoogleApiClient.Builder builder = new GoogleApiClient.Builder(this);
                //builder.AddConnectionCallbacks(this);
                //builder.AddOnConnectionFailedListener(this);
                builder.AddApi(PlusClass.API);
                builder.AddScope(PlusClass.ScopePlusLogin);

                mGoogleApiClient = builder.Build();

                LinearLayout layout = FindViewById<LinearLayout>(Resource.Id.linearLayout4);

                Button button = new Button(this);
                //Does not change
                button.Id = 1;
                button.SetBackgroundResource(Resource.Drawable.gold_plate); // This is a custom button drawable, defined in XML 
                float scale = button.Resources.DisplayMetrics.Density;
                button.SetHeight((int)(75 * scale + 0.5f));
                button.SetWidth((int)(400 * scale + 0.5f));
                int padding = (int)(16 * scale + 0.5f);
                button.Text = "Log Out";

                layout.AddView(button);

                //button.Click += mGoogleSignOut_Click;


            }

        }
        
    }
}