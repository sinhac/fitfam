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
            var userId = Intent.GetStringExtra("userId");

            SetContentView(Resource.Layout.EditEventDetails);

            ImageButton navBarHomeButton = FindViewById<ImageButton>(Resource.Id.navBarHomeButton);
            navBarHomeButton.Click += delegate {
                Intent intent = new Intent(this, typeof(HomepageActivity));
                intent.PutExtra("userId", userId);
                StartActivity(intent);
            };

            ImageButton navBarProfileButton = FindViewById<ImageButton>(Resource.Id.navBarProfileButton);
            navBarProfileButton.Click += delegate {
                StartActivity(typeof(ProfilePageActivity));
            };

            ImageButton navBarNotificationsButton = FindViewById<ImageButton>(Resource.Id.navBarNotificationsButton);
            navBarNotificationsButton.Click += delegate {
                StartActivity(typeof(NotificationsActivity));
            };

            ImageButton navBarScheduleButton = FindViewById<ImageButton>(Resource.Id.navBarScheduleButton);
            navBarScheduleButton.Click += delegate {
                StartActivity(typeof(ScheduleActivity));
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
                StartActivity(typeof(EventDetailsPageActivity));
            };

        }

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

        }
    }
}