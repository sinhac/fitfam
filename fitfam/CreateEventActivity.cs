using Amazon.DynamoDBv2.Model;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms.Platform.Android;

/*
 * FitFam 
 * 
 * Ehsan Ahmed, Jessa Marie Barre, Shannon Fisher, 
 * Josh Jacobson, Korey Prendergast, Chandrika Sinha
 * 4/20/2017
 * 
 * CreateAnEventActivity: create an event
 * user can make a new event that other users can join using this form
 * 
 */

namespace fitfam
{
    [Activity(Label = "CreateEventActivity")]
    public class CreateEventActivity : Activity
    {
        // private member variables
        private string userId;
        private User creator;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CreateEventPage);

            // get information from previous page
            userId = Intent.GetStringExtra("userId") ?? "null";
            var pic = Intent.GetStringExtra("pic") ?? "null";
            var location = Intent.GetStringExtra("location") ?? "null";
            var username = Intent.GetStringExtra("username") ?? "null";
            var genderInt = Intent.GetIntExtra("gender", -1);
            var profileId = Intent.GetStringExtra("profileId") ?? "Null";
            creator = new User(userId, true);

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

            // capture user input for event name
            var eventName = FindViewById<MultiAutoCompleteTextView>(Resource.Id.eventName);
            var eventNameInput = "";
            eventName.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                eventNameInput = e.Text.ToString();
            };

            // capture user input for event location
            var locationText = FindViewById<MultiAutoCompleteTextView>(Resource.Id.location);
            var locationInput = "";
            locationText.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                locationInput = e.Text.ToString();
            };

            // capture user input for event description
            var description = FindViewById<EditText>(Resource.Id.description);
            var descriptionInput = "";
            description.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                descriptionInput = e.Text.ToString();
            };

            // capture user input for preferred activity tags
            var tags = FindViewById<MultiAutoCompleteTextView>(Resource.Id.activityTags);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.ACTIVITIES, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            
            tags.Adapter = adapter;
            tags.SetTokenizer(new MultiAutoCompleteTextView.CommaTokenizer());

            var tagsInput = "";
            List<string> tagsList = new List<string>();
            tags.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                tagsInput = e.Text.ToString();
                Console.WriteLine(tagsInput);
            };

            // enter code for collecting experience level
            
            // date picker
            DatePicker date = FindViewById<DatePicker>(Resource.Id.datePicker1);
            DateTime startInput;
            DateTime endInput;

            // start time
            Spinner startHour = FindViewById<Spinner>(Resource.Id.startHour);

            startHour.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var adapter1 = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.hour_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter1.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            startHour.Adapter = adapter1;

            Spinner startMin = FindViewById<Spinner>(Resource.Id.startMin);

            startMin.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var adapter2 = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.minute_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            startMin.Adapter = adapter2;

            // end time
            Spinner endHour = FindViewById<Spinner>(Resource.Id.endHour);

            endHour.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var adapter3 = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.hour_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter3.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            endHour.Adapter = adapter3;

            Spinner endMin = FindViewById<Spinner>(Resource.Id.endMin);

            endMin.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var adapter4 = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.minute_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter4.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            endMin.Adapter = adapter4;

            // boost functionality for user to input money
            EditText boostText = FindViewById<EditText>(Resource.Id.boostEditText);
            var boostInput = "";
            boostText.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                boostInput = e.Text.ToString();
            };

            // add user input to new entry in database, then redirect */
            Button createEventButton = FindViewById<Button>(Resource.Id.createEventButton);
            createEventButton.Click += delegate {
                // boost input
                if (boostInput == "") { boostInput = "0"; }
                double boost = double.Parse(boostInput, System.Globalization.CultureInfo.InvariantCulture);

                startInput = date.DateTime;
                endInput = date.DateTime;

                string hour = (string)startHour.GetItemAtPosition(startHour.SelectedItemPosition);
                startInput = startInput.AddHours(Convert.ToDouble(hour));
                string minute = (string)startMin.GetItemAtPosition(startMin.SelectedItemPosition);
                startInput = startInput.AddMinutes(Convert.ToDouble(minute));

                char[] delimiters = { ',' };
                string[] tagsArr = tagsInput.Split(delimiters);
                TextInfo myTI = new CultureInfo("en-US",false).TextInfo;
                for (int i = 0; i < tagsArr.Length; i++)
                {
                    Console.WriteLine(tagsArr[i]);
                    tagsList.Add(myTI.ToLower(tagsArr[i]));
                }


                hour = (string)endHour.GetItemAtPosition(endHour.SelectedItemPosition);
                endInput = endInput.AddHours(Convert.ToDouble(hour));
                minute = (string)endMin.GetItemAtPosition(endMin.SelectedItemPosition);
                endInput = endInput.AddMinutes(Convert.ToDouble(minute));
                Event newEvent = new Event(eventNameInput, descriptionInput, locationInput, startInput, endInput, false, tagsList, creator, boost );
                var userFam = creator.UserFam;
                userFam.makeEvent(newEvent);

                var intent = new Intent(this, typeof(EventDetailsPageActivity));
                intent.PutExtra("eventId",newEvent.EventId);
                intent.PutExtra("userId", userId);
                intent.PutExtra("profileId", userId);
                intent.PutExtra("pic", pic);
                intent.PutExtra("location", location);
                intent.PutExtra("username", username);
                intent.PutExtra("gender", genderInt);
                StartActivity(intent);
            };
        }

        // spinner helper
        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
        }
    }
}