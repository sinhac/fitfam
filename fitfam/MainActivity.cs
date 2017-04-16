using Android.App;
using Android.Widget;
using Android.OS;
using Xamarin.Facebook;
using Xamarin.Facebook.AppEvents;

namespace fitfam
{
    [Activity(Label = "fitfam", MainLauncher = true, Icon = "@drawable/mightyMan")]
    public class MainActivity : Activity
    {

        public ICallbackManager CallbackManager { get; private set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            FacebookSdk.SdkInitialize(ApplicationContext);
            AppEventsLogger.ActivateApp(Application);
            var activity = Context as MainActivity;
            var login_button = FindViewById<Xamarin.Facebook.Login.Widget.LoginButton>(Resource.Id.login_button);
            Button signup_button = FindViewById<Button>(Resource.Id.signup_button);
            //var loginManager = new Xamarin.
            login_button.SetReadPermissions("public_profile,user_friends");
            var facebookCallback = new FacebookCallback<LoginResult>()
            {
                HandleSuccess = (LoginResult loginResult) =>
                {
                    login_button.LoginSuccess(loginResult.AccessToken.Token);
                },

                HandleCancel = () =>
                {
                    //App code
                },

                HandleError = (FacebookException ex) =>
                {
                    Console.WriteLine(ex.Message);
                }
            };

            login_button.RegisterCallback(activity.CallbackManager, facebookCallback);
            SetNativeControl(login_button);
            //  login_button.registerCallback(callbackManager, new FacebookCallback<LoginResult>)


            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            login_button.Click += delegate {
                StartActivity(typeof(HomepageActivity));
            };

            signup_button.Click += delegate {
                StartActivity(typeof(HomepageActivity));
            };

            //var request = new Amazon.Runtime.AmazonWebServiceRequest;
            //var request = new Amazon.DynamoDBv2.AmazonDynamoDBRequest();
        }
    }
}
