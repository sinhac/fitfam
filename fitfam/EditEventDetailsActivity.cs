using System;
using Android.App;
using Android.OS;
using Android.Widget;

namespace fitfam
{
    [Activity(Label = "EditEventDetailsActivity")]
    public class EditEventDetailsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.EditEventDetails);

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

            var location = FindViewById<MultiAutoCompleteTextView>(Resource.Id.multiAutoCompleteTextView2);
            var locationInput = "";
            location.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                locationInput = e.Text.ToString();
            };

            var description = FindViewById<EditText>(Resource.Id.editText);
            var descriptionInput = "";
            description.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                descriptionInput = e.Text.ToString();
            };

            //Date picker
            DatePicker date = FindViewById<DatePicker>(Resource.Id.datePicker1);
            DateTime startInput;
            DateTime endInput;

            //Time spinner
            Spinner spinner1 = FindViewById<Spinner>(Resource.Id.spinner1);

            spinner1.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var adapter1 = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.hour_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter1.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner1.Adapter = adapter1;

            Spinner spinner2 = FindViewById<Spinner>(Resource.Id.spinner2);

            spinner2.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var adapter2 = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.minute_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner2.Adapter = adapter2;

            //End time
            Spinner spinner3 = FindViewById<Spinner>(Resource.Id.spinner3);

            spinner3.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var adapter3 = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.hour_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter3.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner3.Adapter = adapter3;

            Spinner spinner4 = FindViewById<Spinner>(Resource.Id.spinner4);

            spinner4.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var adapter4 = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.minute_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter4.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner4.Adapter = adapter4;

            Button save_changes_button = FindViewById<Button>(Resource.Id.save_changes_button);
            save_changes_button.Click += delegate {
                startInput = date.DateTime;
                string hour = (string)spinner1.GetItemAtPosition(spinner1.SelectedItemPosition);
                startInput = startInput.AddHours(Convert.ToDouble(hour));
                string minute = (string)spinner2.GetItemAtPosition(spinner2.SelectedItemPosition);
                startInput = startInput.AddMinutes(Convert.ToDouble(minute));

                endInput = date.DateTime;
                hour = (string)spinner3.GetItemAtPosition(spinner3.SelectedItemPosition);
                endInput = endInput.AddHours(Convert.ToDouble(hour));
                minute = (string)spinner4.GetItemAtPosition(spinner4.SelectedItemPosition);
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