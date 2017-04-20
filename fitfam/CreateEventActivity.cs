using Amazon.DynamoDBv2.Model;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms.Platform.Android;

namespace fitfam
{
    [Activity(Label = "CreateEventActivity")]
    public class CreateEventActivity : Activity
    {
        private string userId;
        private User creator;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            userId = Intent.GetStringExtra("userId") ?? "Null";
            creator = new User(userId, true);
            SetContentView(Resource.Layout.CreateEventPage);

            /* capture user input for event name, location, and description */
            var eventName = FindViewById<MultiAutoCompleteTextView>(Resource.Id.multiAutoCompleteTextView1);
            var eventNameInput = "";
            eventName.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                eventNameInput = e.Text.ToString();
            };

            var location = FindViewById<MultiAutoCompleteTextView>(Resource.Id.multiAutoCompleteTextView2);
            var locationInput = "";
            location.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                locationInput = e.Text.ToString();
            };

            var description = FindViewById<EditText>(Resource.Id.editText1);
            var descriptionInput = "";
            description.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                descriptionInput = e.Text.ToString();
            };

            /* get tags */
            var tags = FindViewById<MultiAutoCompleteTextView>(Resource.Id.multiAutoCompleteTextView3);
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

            EditText boostText = FindViewById<EditText>(Resource.Id.editText2);
            var boostInput = "";
            boostText.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                boostInput = e.Text.ToString();
            };

            /* add user input to new entry in database, then redirect */
            Button button1 = FindViewById<Button>(Resource.Id.button1);
            button1.Click += delegate {
                //BOOST INPUT 
                if (boostInput == "") { boostInput = "0"; }
                double boost = double.Parse(boostInput, System.Globalization.CultureInfo.InvariantCulture);

                startInput = date.DateTime;
                endInput = date.DateTime;

                string hour = (string)spinner1.GetItemAtPosition(spinner1.SelectedItemPosition);
                startInput = startInput.AddHours(Convert.ToDouble(hour));
                string minute = (string)spinner2.GetItemAtPosition(spinner2.SelectedItemPosition);
                startInput = startInput.AddMinutes(Convert.ToDouble(minute));

                char[] delimiters = { ',' };
                string[] tagsArr = tagsInput.Split(delimiters);
                Console.WriteLine("tagsArr count {0}", tagsArr.Length);
                TextInfo myTI = new CultureInfo("en-US",false).TextInfo;
                for (int i = 0; i < tagsArr.Length; i++)
                {
                    Console.WriteLine(tagsArr[i]);
                    tagsList.Add(myTI.ToLower(tagsArr[i]));
                }


                hour = (string)spinner3.GetItemAtPosition(spinner3.SelectedItemPosition);
                endInput = endInput.AddHours(Convert.ToDouble(hour));
                minute = (string)spinner4.GetItemAtPosition(spinner4.SelectedItemPosition);
                endInput = endInput.AddMinutes(Convert.ToDouble(minute));
                Console.WriteLine("userId: {0}", userId);
                Console.WriteLine("Event: {0} {1} {2} {3} {4}", eventNameInput, descriptionInput, locationInput, startInput, endInput);
                foreach(var t in tagsList)
                {
                    Console.WriteLine(t);
                }
                Event newEvent = new Event(eventNameInput, descriptionInput, locationInput, startInput, endInput, false, tagsList, creator, boost );
                Console.WriteLine(creator.UserId);
                var userFam = creator.UserFam;
                userFam.makeEvent(newEvent);

                var eventDetailsActivity = new Intent(this, typeof(EventDetailsPageActivity));
                eventDetailsActivity.PutExtra("eventId",newEvent.EventId);
                eventDetailsActivity.PutExtra("userId", userId);
                StartActivity(eventDetailsActivity);
            };
            
            /* navbar buttons */
            ImageButton imagebutton1 = FindViewById<ImageButton>(Resource.Id.imageButton1);
            imagebutton1.Click += delegate {
                Intent intent = new Intent(this, typeof(HomepageActivity));
                intent.PutExtra("userId", userId);
                StartActivity(intent);
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

        }
    }
}