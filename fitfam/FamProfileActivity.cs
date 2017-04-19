using Android.App;
using Android.OS;
using Android.Widget;

namespace fitfam
{
    [Activity(Label = "Activity1")]
    public class FamProfileActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.FamProfile);

            string groupId = Intent.GetStringExtra("groupId") ?? "Data not available";
            string toast = string.Format("You have been added to {0}", groupId);
            Toast.MakeText(this, toast, ToastLength.Long).Show();

            ImageButton imagebutton1 = FindViewById<ImageButton>(Resource.Id.imageButton1);
            imagebutton1.Click += delegate {
                StartActivity(typeof(HomepageActivity));
            };

            ImageButton imagebutton2 = FindViewById<ImageButton>(Resource.Id.imageButton2);
            imagebutton2.Click += delegate {
                StartActivity(typeof(ProfilePageActivity));
            };

            ImageButton imagebutton3 = FindViewById<ImageButton>(Resource.Id.imageButton3);
            imagebutton3.Click += delegate {
                StartActivity(typeof(NotificationsActivity));
            };

            ImageButton imagebutton4 = FindViewById<ImageButton>(Resource.Id.imageButton4);
            imagebutton4.Click += delegate {
                StartActivity(typeof(ScheduleActivity));
            };

            /*
            Button button1 = FindViewById<Button>(Resource.Id.button1);
            button1.Click += delegate {
                StartActivity(typeof(FamDetailsPageActivity));
            };*/

        }
    }
}
