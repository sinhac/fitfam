using Android.App;
using Android.OS;
using Android.Widget;

namespace fitfam
{
    [Activity(Label = "FindaneventActivity")]
    public class FindAnEventActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FindAnEventForm);

            // Create your application here
            Button button2 = FindViewById<Button>(Resource.Id.button2);

            button2.Click += delegate {
                StartActivity(typeof(MatchesActivity));
            };

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
            
        }
    }
}