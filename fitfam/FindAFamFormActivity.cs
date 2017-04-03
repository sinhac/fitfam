using Android.App;
using Android.OS;
using Android.Widget;

namespace fitfam
{
    [Activity(Label = "FindafamformActivity")]
    public class FindAFamFormActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Group fam = new Group();
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FindAFamForm);

            // Create your application here

            var activity = FindViewById<EditText>(Resource.Id.activity);
            var activityTag = "";
            activity.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                activityTag = e.Text.ToString();
            };

            var cityZip = FindViewById<EditText>(Resource.Id.cityzip);
            var cityZipInput = "";
            activity.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                cityZipInput = e.Text.ToString();
            };

            var experience = FindViewById<EditText>(Resource.Id.experience);
            var experienceInput = "";
            activity.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                experienceInput = e.Text.ToString();
            };

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
