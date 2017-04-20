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
using Amazon.DynamoDBv2;
using System.Collections.Generic;
using Amazon.DynamoDBv2.Model;

namespace fitfam
{
    [Activity(Label = "ProfilePageActivity")]
    public class ProfilePageActivity : Activity
    {
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
            userId = Intent.GetStringExtra("userId") ?? "Null";
            profileId = Intent.GetStringExtra("profileId") ?? "Null";
            try
            {
               
                AWSClient client = new AWSClient(Amazon.RegionEndpoint.USEast1);
                AmazonDynamoDBClient dbclient = client.getDynamoDBClient();
                string tableName = "fitfam-mobilehub-2083376203-users";
                var Key = new Dictionary<string, AttributeValue>() { { "userId", new AttributeValue { S = userId } } };
                var request = client.makeGetRequest(tableName, Key);
                var task = await client.GetItemAsync(dbclient, request);

                SetContentView(Resource.Layout.UserProfilePage);
                User currentUser = new User(profileId, true);
                System.Console.WriteLine("User " + profileId + " Name " + task["username"]);

                TextView username = FindViewById<TextView>(Resource.Id.textView1);
                username.Text = task["username"].S;

                LinearLayout layout = FindViewById<LinearLayout>(Resource.Id.linearLayout4);
                TextView name = new TextView(this);
                //name.Text = "Shannon";
                name.Text = task["username"].S;
                layout.AddView(name);

                TextView bio = new TextView(this);
                bio.Text = task["bio"].S;
                //Console.WriteLine("PROFILE BIO {0}", currentUser.Bio);
                layout.AddView(bio);
                /*
                TextView activities = new TextView(this);
                activities.Text = ("Activities: " + string.Join(", ", currentUser.Activities));
                layout.AddView(activities);
                */
                // Create your application here
                Button myfams_button = FindViewById<Button>(Resource.Id.myFamButton);
                myfams_button.Click += delegate
                {
                    StartActivity(typeof(FamProfileActivity));
                };

                ImageButton imagebutton1 = FindViewById<ImageButton>(Resource.Id.imageButton1);
                imagebutton1.Click += delegate {

                    Intent intent = new Intent(this, typeof(HomepageActivity));
                    intent.PutExtra("userId", userId);
                    Console.WriteLine("go home");
                    StartActivity(intent);
                };

                ImageButton imagebutton2 = FindViewById<ImageButton>(Resource.Id.imageButton2);
                imagebutton2.Click += delegate {
                    Intent intent = new Intent(this, typeof(ProfilePageActivity));
                    intent.PutExtra("userId", userId);
                    intent.PutExtra("profileId", userId);
                    intent.PutExtra("bio", bio.Text);
                    intent.PutExtra("username", username.Text);
                    //  intent.PutExtra("gender", );
                    //intent.Put("activities", user.Activities);
                    StartActivity(intent);
                };

                ImageButton imagebutton3 = FindViewById<ImageButton>(Resource.Id.imageButton3);
                imagebutton3.Click += delegate {
                    StartActivity(typeof(NotificationsActivity));
                };

                ImageButton imagebutton4 = FindViewById<ImageButton>(Resource.Id.imageButton4);
                imagebutton4.Click += delegate {
                    StartActivity(typeof(ScheduleActivity));
                };


                if (userId == profileId)
                {
                    Button editprofile_button = FindViewById<Button>(Resource.Id.editProfileButton);
                    editprofile_button.Click += delegate
                    {
                        Intent intent = new Intent(this, typeof(EditProfilePageActivity));
                        intent.PutExtra("userId", userId);
                        StartActivity(intent);
                    };

                    GoogleApiClient.Builder builder = new GoogleApiClient.Builder(this);
                    //builder.AddConnectionCallbacks(this);
                    //builder.AddOnConnectionFailedListener(this);
                    builder.AddApi(PlusClass.API);
                    builder.AddScope(PlusClass.ScopePlusLogin);

                    mGoogleApiClient = builder.Build();
                    mGoogleApiClient.Connect();

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
                    button.Click += delegate
                    {
                        mGoogleApiClient.ClearDefaultAccountAndReconnect();
                        StartActivity(typeof(MainActivity));
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception {0}\n\n\n\n{1}", ex.Message, ex.Source);
                Intent intent = new Intent(this, typeof(HomepageActivity));
                intent.PutExtra("userId", userId);
                Console.WriteLine("go home fitfam you're drunk");
                StartActivity(intent);
            }
            

        }
        
    }
}