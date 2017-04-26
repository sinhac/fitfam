using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Gms.Common.Apis;
using Android.Gms.Common;
using Android.Gms.Plus;
using Android.Gms.Plus.Model.People;

/*
 * FitFam 
 * 
 * Ehsan Ahmed, Jessa Marie Barre, Shannon Fisher, 
 * Josh Jacobson, Korey Prendergast, Chandrika Sinha
 * 4/20/2017
 * 
 * MainActivity: The initial page
 * Uses Google's sign up functionality
 * 
 */

namespace fitfam
{
    [Activity(Label = "fitfam", MainLauncher = true, Icon = "@drawable/mightyMan")]
    public class MainActivity : Activity, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
    {   
        // private member variables
        private GoogleApiClient mGoogleApiClient;
        private SignInButton mGoogleSignIn;
        private bool mIntentInProgress;
        private bool mSignInClicked;
        private bool mInfoPopulated;
        private ConnectionResult mConnectionResult;

        // initial user interface and begin functions
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            mGoogleSignIn = FindViewById<SignInButton>(Resource.Id.sign_in_button);
            mGoogleSignIn.Click += mGoogleSignIn_Click;

            GoogleApiClient.Builder builder = new GoogleApiClient.Builder(this);
            builder.AddConnectionCallbacks(this);
            builder.AddOnConnectionFailedListener(this);
            builder.AddApi(PlusClass.API);
            builder.AddScope(PlusClass.ScopePlusProfile);
            builder.AddScope(PlusClass.ScopePlusLogin);
            mGoogleApiClient = builder.Build();
        }

        // initial signal to connect to the Google API and sign in
        void mGoogleSignIn_Click(object sender, EventArgs e)
        {
            if (!mGoogleApiClient.IsConnecting)
            {
                mSignInClicked = true;
                ResolveSignInError();
            }
        }

        // if client is connected, no need to resolve errors
        // else attempt to reolve by sending for result
        private void ResolveSignInError()
        {
            if (mGoogleApiClient.IsConnected)
            {
                return;
            }

            if (mConnectionResult.HasResolution)
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

        // shows whether signing in was succesful
        // attempts to reconnect
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

        // calls Google API to attempt connection on start
        protected override void OnStart()
        {
            base.OnStart();
            mGoogleApiClient.Connect();
        }

        // disconnects Google connection on stop
        protected override void OnStop()
        {
            base.OnStop();
            if (mGoogleApiClient.IsConnected)
            {
                mGoogleApiClient.Disconnect();
            }
        }

        // passes Google user IDs and information to other pages
        public void OnConnected(Bundle connectionHint)
        {
            mSignInClicked = false;

            if (PlusClass.PeopleApi.GetCurrentPerson(mGoogleApiClient) != null)
            {
                IPerson plusUser = PlusClass.PeopleApi.GetCurrentPerson(mGoogleApiClient);
                Intent intent = new Intent(this, typeof(HomepageActivity));
                intent.PutExtra("userId", plusUser.Id);
                intent.PutExtra("username", plusUser.DisplayName);
                intent.PutExtra("gender", plusUser.Gender);
                intent.PutExtra("pic", plusUser.Image.Url);
                intent.PutExtra("location", plusUser.CurrentLocation);
                StartActivity(intent);
            }
        }

        // handles code if connection takes too long
        public void OnConnectionSuspended(int cause)
        {
            Console.WriteLine("CONNECTION SUSPENDED: cause " + cause);
            throw new NotImplementedException();
        }

        // handles connection failures
        // stores connection result for future
        // resolves errors until user is signed in
        public void OnConnectionFailed(ConnectionResult result)
        {
            if (!mIntentInProgress)
            {
                mConnectionResult = result;

                if (mSignInClicked)
                {
                    ResolveSignInError();
                }
            }
        }
    }
}
