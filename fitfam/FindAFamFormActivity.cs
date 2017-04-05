using Android.App;
using Android.OS;
using Android.Widget;
using System;

namespace fitfam
{
    [Activity(Label = "FindafamformActivity")]
    public class FindAFamFormActivity : Activity
    {
        private object spinner_ItemSelected;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FindAFamForm);

            // Create your application here
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner1);

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.experience_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

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
        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            string toast = string.Format("The experience is {0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }
    }
}