using System;
using Android.App;
using Android.OS;
using Android.Widget;
using Android.Content;

namespace fitfam
{
    [Activity(Label = "EditEventDetailsActivity")]
    public class EditEventDetailsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EditEventDetails);
            
            // get information from previous page
            var userId = Intent.GetStringExtra("userId") ?? "null";
            var pic = Intent.GetStringExtra("pic") ?? "null";
            var location = Intent.GetStringExtra("location") ?? "null";
            var username = Intent.GetStringExtra("username") ?? "null";
            var genderInt = Intent.GetIntExtra("gender", -1);
            var profileId = Intent.GetStringExtra("profileId") ?? "Null";

            // navbar buttons
            ImageButton homepageButton = FindViewById<ImageButton>(Resource.Id.homepageButton);
            homepageButton.Click += delegate
            {
                Intent intent = new Intent(this, typeof(HomepageActivity));
                intent.PutExtra("userId", userId);
                intent.PutExtra("profileId", userId);
                intent.PutExtra("pic", pic);
                intent.PutExtra("location", location);
                intent.PutExtra("username", username);
                intent.PutExtra("gender", genderInt);
                StartActivity(intent);
            };

            ImageButton profileButton = FindViewById<ImageButton>(Resource.Id.profileButton);
            profileButton.Click += delegate
            {
                Intent intent = new Intent(this, typeof(ProfilePageActivity));
                intent.PutExtra("userId", userId);
                intent.PutExtra("profileId", userId);
                intent.PutExtra("username", username);
                intent.PutExtra("pic", pic);
                intent.PutExtra("location", location);
                intent.PutExtra("gender", genderInt);
                StartActivity(intent);
            };

            ImageButton notificationsButton = FindViewById<ImageButton>(Resource.Id.notificationsButton);
            notificationsButton.Click += delegate
            {
                Intent intent = new Intent(this, typeof(NotificationsActivity));
                intent.PutExtra("userId", userId);
                intent.PutExtra("profileId", userId);
                intent.PutExtra("pic", pic);
                intent.PutExtra("location", location);
                intent.PutExtra("username", username);
                intent.PutExtra("gender", genderInt);
                StartActivity(intent);

            };

            ImageButton scheduleButton = FindViewById<ImageButton>(Resource.Id.scheduleButton);
            scheduleButton.Click += delegate
            {
                Intent intent = new Intent(this, typeof(ScheduleActivity));
                intent.PutExtra("userId", userId);
                intent.PutExtra("profileId", userId);
                intent.PutExtra("pic", pic);
                intent.PutExtra("location", location);
                intent.PutExtra("username", username);
                intent.PutExtra("gender", genderInt);
                StartActivity(intent);
            };

            var locationEditView = FindViewById<MultiAutoCompleteTextView>(Resource.Id.locationEditView);
            var locationInput = "";
            locationEditView.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                locationInput = e.Text.ToString();
            };

            var descriptionEditText = FindViewById<EditText>(Resource.Id.descriptionEditText);
            var descriptionInput = "";
            descriptionEditText.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                descriptionInput = e.Text.ToString();
            };

            //Date picker
            DatePicker date = FindViewById<DatePicker>(Resource.Id.datePicker);
            DateTime startInput;
            DateTime endInput;

            //Time spinner
            Spinner startTimeHourSpinner = FindViewById<Spinner>(Resource.Id.startTimeHourSpinner);

            startTimeHourSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var adapter1 = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.hour_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter1.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            startTimeHourSpinner.Adapter = adapter1;

            Spinner startTimeMinuteSpinner = FindViewById<Spinner>(Resource.Id.startTimeMinuteSpinner);

            startTimeMinuteSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var adapter2 = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.minute_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            startTimeMinuteSpinner.Adapter = adapter2;

            //End time
            Spinner endTimeHourSpinner = FindViewById<Spinner>(Resource.Id.endTimeHourSpinner);

            endTimeHourSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var adapter3 = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.hour_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter3.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            endTimeHourSpinner.Adapter = adapter3;

            Spinner endTimeMinuteSpinner = FindViewById<Spinner>(Resource.Id.endTimeMinuteSpinner);

            endTimeMinuteSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var adapter4 = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.minute_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter4.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            endTimeMinuteSpinner.Adapter = adapter4;

            Button save_changes_button = FindViewById<Button>(Resource.Id.save_changes_button);
            save_changes_button.Click += delegate {
                startInput = date.DateTime;
                string hour = (string)startTimeHourSpinner.GetItemAtPosition(startTimeHourSpinner.SelectedItemPosition);
                startInput = startInput.AddHours(Convert.ToDouble(hour));
                string minute = (string)startTimeMinuteSpinner.GetItemAtPosition(startTimeMinuteSpinner.SelectedItemPosition);
                startInput = startInput.AddMinutes(Convert.ToDouble(minute));

                endInput = date.DateTime;
                hour = (string)endTimeHourSpinner.GetItemAtPosition(endTimeHourSpinner.SelectedItemPosition);
                endInput = endInput.AddHours(Convert.ToDouble(hour));
                minute = (string)endTimeMinuteSpinner.GetItemAtPosition(endTimeMinuteSpinner.SelectedItemPosition);
                endInput = endInput.AddMinutes(Convert.ToDouble(minute));

                // Event newEvent = new Event(eventNameInput, descriptionInput, locationInput, startInput, endInput, true, tagsList, new fitfam.User("fakeCreator"));
                // var eventDetailsActivity = new Intent(this, typeof(EventDetailsPageActivity));
                // eventDetailsActivity.PutExtra("eventId", newEvent.EventId);


                Intent intent = new Intent(this, typeof(EventDetailsPageActivity));
                intent.PutExtra("userId", userId);
                intent.PutExtra("profileId", userId);
                intent.PutExtra("pic", pic);
                intent.PutExtra("location", location);
                intent.PutExtra("username", username);
                intent.PutExtra("gender", genderInt);
                StartActivity(intent);
            };

        }

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

        }
    }
}