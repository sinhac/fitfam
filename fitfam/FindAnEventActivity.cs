using Amazon.DynamoDBv2.Model;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;

/*
 * FitFam 
 * 
 * Ehsan Ahmed, Jessa Marie Barre, Shannon Fisher, 
 * Josh Jacobson, Korey Prendergast, Chandrika Sinha
 * 4/20/2017
 * 
 * FindAnEventActivity: The page that allows users to search events
 * users can search for individual events
 * 
 */

namespace fitfam
{
    [Activity(Label = "FindaneventActivity")]
    public class FindAnEventActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FindAnEventForm);

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

            // capture activity tag input
            var tags = FindViewById<MultiAutoCompleteTextView>(Resource.Id.activityTags);
            var adapter2 = ArrayAdapter.CreateFromResource(this, Resource.Array.ACTIVITIES, Android.Resource.Layout.SimpleSpinnerItem);
            adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            tags.Adapter = adapter2;
            tags.SetTokenizer(new MultiAutoCompleteTextView.CommaTokenizer());

            var tagsInput = "";
            string[] tagsArr;
            List<string> tagsList = new List<string>();
            tags.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                tagsInput = e.Text.ToString();
            };

            // experience level
            Spinner experienceSpinner = FindViewById<Spinner>(Resource.Id.experienceSpinner);
            experienceSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.experience_array, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            experienceSpinner.Adapter = adapter;

            //Date picker
            DatePicker dateStart = FindViewById<DatePicker>(Resource.Id.startDate);
            DatePicker dateEnd = FindViewById<DatePicker>(Resource.Id.endDate);
            DateTime startInput;
            DateTime endInput;

            //Start Time
            Spinner startHourSpinner = FindViewById<Spinner>(Resource.Id.startHour);

            startHourSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var startHourAdapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.hour_array, Android.Resource.Layout.SimpleSpinnerItem);

            startHourAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            startHourSpinner.Adapter = startHourAdapter;

            Spinner startMinSpinner = FindViewById<Spinner>(Resource.Id.startMin);

            startMinSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var startMinAdapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.minute_array, Android.Resource.Layout.SimpleSpinnerItem);

            startMinAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            startMinSpinner.Adapter = startMinAdapter;

            // End
            Spinner endHourSpinner = FindViewById<Spinner>(Resource.Id.endHour);

            endHourSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var endHourAdapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.hour_array, Android.Resource.Layout.SimpleSpinnerItem);

            endHourAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            endHourSpinner.Adapter = endHourAdapter;

            Spinner endMinSpinner = FindViewById<Spinner>(Resource.Id.endMin);

            endMinSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var endMinAdapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.minute_array, Android.Resource.Layout.SimpleSpinnerItem);

            endMinAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            endMinSpinner.Adapter = endMinAdapter;

            // Find Events button functionality
            Button findEventButton = FindViewById<Button>(Resource.Id.findEventButton);
            findEventButton.Click += async delegate
            {
                // tags
                char[] delimiters = { ',', '\t', '\n' };
                tagsArr = tagsInput.Split(delimiters);
                for (int i = 0; i < tagsArr.Length; i++)
                {
                    if (tagsArr[i] != "")
                    {
                        tagsList.Add(tagsArr[i]);
                    }
                }

                // experience
                var experienceLevel = (string)experienceSpinner.GetItemAtPosition(experienceSpinner.SelectedItemPosition);

                // date
                //startInput = dateStart.DateTime;
                //endInput = dateEnd.DateTime;

                //// start time
                //string hour = (string)startHourSpinner.GetItemAtPosition(startHourSpinner.SelectedItemPosition);
                //startInput = startInput.AddHours(Convert.ToDouble(hour));
                //string minute = (string)startMinSpinner.GetItemAtPosition(startMinSpinner.SelectedItemPosition);
                //startInput = startInput.AddMinutes(Convert.ToDouble(minute));

                //// end time
                //hour = (string)endHourSpinner.GetItemAtPosition(endHourSpinner.SelectedItemPosition);
                //endInput = endInput.AddHours(Convert.ToDouble(hour));
                //minute = (string)endMinSpinner.GetItemAtPosition(endMinSpinner.SelectedItemPosition);
                //endInput = endInput.AddMinutes(Convert.ToDouble(minute));

                var awsClient = new AWSClient(Amazon.RegionEndpoint.USEast1);
                var client = awsClient.getDynamoDBClient();

                var request = new ScanRequest
                {
                    TableName = "fitfam-mobilehub-2083376203-events",
                };
                var response = await client.ScanAsync(request);
                var result = response.Items;
                System.Console.WriteLine("Looking for event matches");

                var results = new List<string>();

                // algorithm to find matches
                int numRows = 0;
                foreach (Dictionary<string, AttributeValue> item in result)
                {
                    numRows++;
                    int numMatches = 0;
                    string eventId = item["eventId"].S;

                    foreach (var kvp in item)
                    {
                        if (kvp.Key == "tags")
                        {
                            var eventTags = kvp.Value.SS;
                            foreach (var t in eventTags)
                            {
                                string s = t;
                                foreach (string tag in tagsList)
                                {
                                    //Console.WriteLine(s + " compared to " + tag);
                                    string lowerS = s.ToLower().Replace(" ", "");
                                    string lowerTag = tag.ToLower().Replace(" ", "");
                                    if (lowerS == lowerTag && s != "" && tag != "")
                                    {
                                        numMatches++;
                                    }
                                }
                            }
                        }
                        else if (kvp.Key == "experienceLevels")
                        {
                            var experienceLevels = kvp.Value.SS;
                            int numExp = 0;
                            foreach (var e in experienceLevels)
                            {
                                string s = e;
                                numExp++;
                                if (s[0] == experienceLevel[0])
                                {
                                    numMatches++;
                                }
                            }
                        }
                    }
                    if (numMatches >= 2)
                    {
                        System.Console.WriteLine("Number of event matches found: " + numMatches + " Event "+ item["eventId"].S.ToString()+" added");
                        results.Add(item["eventId"].S);
                    }
                }

                Intent intent = new Intent(this, typeof(EventMatchesActivity));
                intent.PutExtra("matches", results.ToArray());
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