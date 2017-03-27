using Android.App;
using Android.Widget;
using Android.OS;

namespace fitfam
{
    [Activity(Label = "fitfam", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            DynamoDB database = new DynamoDB("AKIAJFNNEKKTWKG5IMJQ", "4hxybS1Lrny5Mn3Ynmy5y88tUNJNrrbu4UrvdlZw", Amazon.RegionEndpoint.USEast1);
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
        }
    }
}

