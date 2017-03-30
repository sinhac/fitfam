using Android.App;
using Android.OS;
using Android.Widget;

namespace fitfam
{
    [Activity(Label = "FindafamformActivity")]
    public class FindafamformActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Findafamform);

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
                StartActivity(typeof(matchesActivity));
            };

            Finish();
        }
    }
}