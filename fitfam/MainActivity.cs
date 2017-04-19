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
    [Activity(Label = "fitfam", MainLauncher = true, Icon = "@drawable/mightyMan")]
    public class MainActivity : Activity, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
    {
        private GoogleApiClient mGoogleApiClient;
        //private IGoogleApiClient mGoogleApiClient;
        private SignInButton mGoogleSignIn;
        private bool mIntentInProgress;
        private bool mSignInClicked;
        private bool mInfoPopulated;
        private ConnectionResult mConnectionResult;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            mGoogleSignIn = FindViewById<SignInButton>(Resource.Id.sign_in_button);
            mGoogleSignIn.Click += mGoogleSignIn_Click;
            

            GoogleApiClient.Builder builder = new GoogleApiClient.Builder(this);
            builder.AddConnectionCallbacks(this);
            builder.AddOnConnectionFailedListener(this);
            builder.AddApi(PlusClass.API);
            builder.AddScope(PlusClass.ScopePlusProfile);
            builder.AddScope(PlusClass.ScopePlusLogin);

            mGoogleApiClient = builder.Build();

            // Get our button from the layout resource,
            // and attach an event to it
            Button login_button = FindViewById<Button>(Resource.Id.login_button);
            Button signup_button = FindViewById<Button>(Resource.Id.signup_button);
            
            login_button.Click += delegate {
                StartActivity(typeof(HomepageActivity));
            };

            signup_button.Click += delegate {
                StartActivity(typeof(HomepageActivity));
            };

            //var request = new Amazon.Runtime.AmazonWebServiceRequest;
            //var request = new Amazon.DynamoDBv2.AmazonDynamoDBRequest();
        }

        void mGoogleSignIn_Click(object sender, EventArgs e)
        {
            if(!mGoogleApiClient.IsConnecting)
            {
                mSignInClicked = true;
                ResolveSignInError();
            }
        }

        private void ResolveSignInError()
        {
            if(mGoogleApiClient.IsConnected)
            {
                //No need to resolve errors
                return;
            }

            if(mConnectionResult.HasResolution)
            {
                try
                {
                    mIntentInProgress = true;
                    StartIntentSenderForResult(mConnectionResult.Resolution.IntentSender, 0, null, 0, 0, 0);
                }
                catch
                {
                    mIntentInProgress = false;
                    mGoogleApiClient.Connect();
                }
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == 0)
            {

                if (resultCode != Result.Ok)
                {
                    mSignInClicked = false;
                }

                mIntentInProgress = false;

                if (!mGoogleApiClient.IsConnecting)
                {
                    mGoogleApiClient.Connect();
                }
            }
        }

        protected override void OnStart()
        {
            base.OnStart();
            mGoogleApiClient.Connect();
        }

        protected override void OnStop()
        {
            base.OnStop();
            if (mGoogleApiClient.IsConnected)
            {
                mGoogleApiClient.Disconnect();
            }
        }

        public void OnConnected(Bundle connectionHint)
        {
            mSignInClicked = false;

            if (PlusClass.PeopleApi.GetCurrentPerson(mGoogleApiClient) != null)
            {
                IPerson plusUser = PlusClass.PeopleApi.GetCurrentPerson(mGoogleApiClient);
                Console.WriteLine("USER NAME: "+plusUser.DisplayName);
                Intent intent = new Intent(this, typeof(HomepageActivity));
                intent.PutExtra("userId", plusUser.Id);
                StartActivity(intent);
            }
        }

        public void OnConnectionSuspended(int cause)
        {
            throw new NotImplementedException();
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            if (!mIntentInProgress)
            {
                // Store the ConnectionResult so that we can use 
                // it later when the user clicks 'sign-in'
                mConnectionResult = result;

                if (mSignInClicked)
                {
                    // The user has already clicked 'sign-in' 
                    // so we attempt to resolve all errors 
                    // until the user is signed in
                    ResolveSignInError();
                }
            }
        }
    }
}
