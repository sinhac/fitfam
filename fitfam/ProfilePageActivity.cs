using System;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Gms.Common.Apis;
using Android.Gms.Common;
using Android.Gms.Plus;
using Amazon.DynamoDBv2;
using System.Collections.Generic;
using Amazon.DynamoDBv2.Model;

/*
 * FitFam 
 * 
 * Ehsan Ahmed, Jessa Marie Barre, Shannon Fisher, 
 * Josh Jacobson, Korey Prendergast, Chandrika Sinha
 * 4/20/2017
 * 
 * ProfilePageActivity: The profile page
 * User can see and edit user profile
 * 
 */

namespace fitfam
{
    [Activity(Label = "ProfilePageActivity")]
    public class ProfilePageActivity : Activity
    {
        // private member variables
        private string userId;
        private string profileId;
        GoogleApiClient mGoogleApiClient;
        private SignInButton mGoogleSignOut;
        private bool mIntentInProgress;
        private bool mSignOutClicked;
        private bool mInfoPopulated;
        private ConnectionResult mConnectionResult;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.UserProfilePage);

            // get information from previous page
            userId = Intent.GetStringExtra("userId") ?? "null";
            var pic = Intent.GetStringExtra("pic") ?? "null";
            var location = Intent.GetStringExtra("location") ?? "null";
            var username = Intent.GetStringExtra("username") ?? "null";
            var genderInt = Intent.GetIntExtra("gender", -1);
            profileId = Intent.GetStringExtra("profileId") ?? "Null";

            // navbar buttons
            ImageButton homepageButton = FindViewById<ImageButton>(Resource.Id.homepageButton);
            homepageButton.Click += delegate
            {
                Intent intent = new Intent(this, typeof(HomepageActivity));
                intent.PutExtra("userId", userId);
                intent.PutExtra("profileId", userId);
                intent.PutExtra("pic", pic);
                intent.PutExtra("location", location);
                intent.PutExtra("username", username);
                intent.PutExtra("gender", genderInt);
                StartActivity(intent);
            };

            ImageButton profileButton = FindViewById<ImageButton>(Resource.Id.profileButton);
            profileButton.Click += delegate
            {
                Intent intent = new Intent(this, typeof(ProfilePageActivity));
                intent.PutExtra("userId", userId);
                intent.PutExtra("profileId", userId);
                intent.PutExtra("username", username);
                intent.PutExtra("pic", pic);
                intent.PutExtra("location", location);
                intent.PutExtra("gender", genderInt);
                StartActivity(intent);
            };

            ImageButton notificationsButton = FindViewById<ImageButton>(Resource.Id.notificationsButton);
            notificationsButton.Click += delegate
            {
                Intent intent = new Intent(this, typeof(NotificationsActivity));
                intent.PutExtra("userId", userId);
                intent.PutExtra("profileId", userId);
                intent.PutExtra("pic", pic);
                intent.PutExtra("location", location);
                intent.PutExtra("username", username);
                intent.PutExtra("gender", genderInt);
                StartActivity(intent);

            };

            ImageButton scheduleButton = FindViewById<ImageButton>(Resource.Id.scheduleButton);
            scheduleButton.Click += delegate
            {
                Intent intent = new Intent(this, typeof(ScheduleActivity));
                intent.PutExtra("userId", userId);
                intent.PutExtra("profileId", userId);
                intent.PutExtra("pic", pic);
                intent.PutExtra("location", location);
                intent.PutExtra("username", username);
                intent.PutExtra("gender", genderInt);
                StartActivity(intent);
            };


            // get info from user table and display on page
            try
            {
                AWSClient client = new AWSClient(Amazon.RegionEndpoint.USEast1);
                AmazonDynamoDBClient dbclient = client.getDynamoDBClient();
                string tableName = "fitfam-mobilehub-2083376203-users";
                var Key = new Dictionary<string, AttributeValue>() { { "userId", new AttributeValue { S = userId } } };
                var request = client.makeGetRequest(tableName, Key);
                var task = await client.GetItemAsync(dbclient, request);

                User currentUser = new User(profileId, true);
                
                TextView userName = FindViewById<TextView>(Resource.Id.textView1);
                userName.Text = task["username"].S;

                LinearLayout layout = FindViewById<LinearLayout>(Resource.Id.linearLayout4);
                TextView name = new TextView(this);
                name.Text = task["username"].S;
                layout.AddView(name);

                TextView bio = new TextView(this);
                bio.Text = task["bio"].S;
                layout.AddView(bio);

                Button myFamsButton = FindViewById<Button>(Resource.Id.myFamsButton);
                myFamsButton.Click += delegate
                {
                    Intent intent = new Intent(this, typeof(FamProfileActivity));
                    intent.PutExtra("userId", userId);
                    intent.PutExtra("profileId", userId);
                    intent.PutExtra("pic", pic);
                    intent.PutExtra("location", location);
                    intent.PutExtra("username", username);
                    intent.PutExtra("gender", genderInt);
                    StartActivity(intent);
                };

                
                if (userId == profileId)
                {
                    Button editProfileButton = FindViewById<Button>(Resource.Id.editProfileButton);
                    editProfileButton.Click += delegate
                    {
                        Intent intent = new Intent(this, typeof(EditProfilePageActivity));
                        intent.PutExtra("userId", userId);
                        intent.PutExtra("profileId", userId);
                        intent.PutExtra("pic", pic);
                        intent.PutExtra("location", location);
                        intent.PutExtra("username", username);
                        intent.PutExtra("gender", genderInt);
                        StartActivity(intent);
                    };

                    GoogleApiClient.Builder builder = new GoogleApiClient.Builder(this);
                    builder.AddApi(PlusClass.API);
                    builder.AddScope(PlusClass.ScopePlusLogin);

                    mGoogleApiClient = builder.Build();
                    mGoogleApiClient.Connect();

                    Button logoutButton = new Button(this);
                    logoutButton.Id = 1;
                    logoutButton.SetBackgroundResource(Resource.Drawable.gold_plate);
                    float scale = logoutButton.Resources.DisplayMetrics.Density;
                    logoutButton.SetHeight((int)(75 * scale + 0.5f));
                    logoutButton.SetWidth((int)(400 * scale + 0.5f));
                    int padding = (int)(16 * scale + 0.5f);
                    logoutButton.Text = "Log Out";
                    layout.AddView(logoutButton);
                    logoutButton.Click += delegate
                    {
                        mGoogleApiClient.ClearDefaultAccountAndReconnect();
                        StartActivity(typeof(MainActivity));
                    };
                }
            }
            catch (Exception ex)
            {
                Intent intent = new Intent(this, typeof(HomepageActivity));
                intent.PutExtra("userId", userId);
                intent.PutExtra("profileId", userId);
                intent.PutExtra("pic", pic);
                intent.PutExtra("location", location);
                intent.PutExtra("username", username);
                intent.PutExtra("gender", genderInt);
                StartActivity(intent);
            }
        }
    }
}