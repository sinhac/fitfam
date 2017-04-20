using Amazon.DynamoDBv2.Model;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace fitfam
{
    [Activity(Label = "FindaneventActivity")]
    public class FindAnEventActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FindAnEventForm);

            string userId = Intent.GetStringExtra("userId");
            User user = new User(userId, false);
            // Create your application here
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner5);

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.experience_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            var tags = FindViewById<MultiAutoCompleteTextView>(Resource.Id.multiAutoCompleteTextView1);
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
            //char[] delimiters = { ',', '\t', '\n' };
            //tagsArr = tagsInput.Split(delimiters);
            //for (int i = 0; i < tagsArr.Length; i++)
            ///{
            //    tagsList.Add(tagsArr[i]);
            //}

            var cityZip = FindViewById<EditText>(Resource.Id.autoCompleteTextView1);
            var cityZipInput = "";
            cityZip.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                cityZipInput = e.Text.ToString();
            };

            //Date picker
            DatePicker dateStart = FindViewById<DatePicker>(Resource.Id.datePicker1);
            DatePicker dateEnd = FindViewById<DatePicker>(Resource.Id.datePicker2);
            DateTime startInput;
            DateTime endInput;

            //Time spinner
            Spinner startHourSpinner = FindViewById<Spinner>(Resource.Id.spinner1);

            startHourSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var startHourAdapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.hour_array, Android.Resource.Layout.SimpleSpinnerItem);

            startHourAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            startHourSpinner.Adapter = startHourAdapter;

            Spinner startMinSpinner = FindViewById<Spinner>(Resource.Id.spinner2);

            startMinSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var startMinAdapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.minute_array, Android.Resource.Layout.SimpleSpinnerItem);

            startMinAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            startMinSpinner.Adapter = startMinAdapter;

            // End
            Spinner endHourSpinner = FindViewById<Spinner>(Resource.Id.spinner3);

            endHourSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var endHourAdapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.hour_array, Android.Resource.Layout.SimpleSpinnerItem);

            endHourAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            endHourSpinner.Adapter = endHourAdapter;

            Spinner endMinSpinner = FindViewById<Spinner>(Resource.Id.spinner4);

            endMinSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var endMinAdapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.minute_array, Android.Resource.Layout.SimpleSpinnerItem);

            endMinAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            endMinSpinner.Adapter = endMinAdapter;

            Button findEventButton = FindViewById<Button>(Resource.Id.button2);

            findEventButton.Click += delegate {
                char[] delimiters = { ',', '\t', '\n' };
                tagsArr = tagsInput.Split(delimiters);
                for (int i = 0; i < tagsArr.Length; i++)
                {
                    tagsList.Add(tagsArr[i]);
                }

                var experienceLevel = (string)spinner.GetItemAtPosition(spinner.SelectedItemPosition);

                startInput = dateStart.DateTime;
                endInput = dateEnd.DateTime;

                string hour = (string)startHourSpinner.GetItemAtPosition(startHourSpinner.SelectedItemPosition);
                startInput = startInput.AddHours(Convert.ToDouble(hour));
                string minute = (string)startMinSpinner.GetItemAtPosition(startMinSpinner.SelectedItemPosition);
                startInput = startInput.AddMinutes(Convert.ToDouble(minute));

                hour = (string)endHourSpinner.GetItemAtPosition(endHourSpinner.SelectedItemPosition);
                endInput = endInput.AddHours(Convert.ToDouble(hour));
                minute = (string)endMinSpinner.GetItemAtPosition(endMinSpinner.SelectedItemPosition);
                endInput = endInput.AddMinutes(Convert.ToDouble(minute));

                /*var results = new List<string>();
                var awsClient = new AWSClient(Amazon.RegionEndpoint.USEast1);
                var client = awsClient.getDynamoDBClient();

                var request = new ScanRequest
                {
                    TableName = "fitfam-mobilehub-2083376203-events",
                    //ProjectionExpression = "groupId, tags, experienceLevels"
                };
                var response = await client.ScanAsync(request);
                var result = response.Items;


                foreach (Dictionary<string, AttributeValue> item in result)
                {
                    int numMatches = 0;
                    string groupId = item["groupId"].S;

                    foreach (var kvp in item)
                    {
                        if (kvp.Key == "tags")
                        {
                            var groupTags = kvp.Value.L;
                            foreach (var t in groupTags)
                            {
                                string s = t.S;
                                foreach (string tag in tagsList)
                                {
                                    Console.WriteLine(s + " compared to " + tag);
                                    if (s.ToLower() == tag.ToLower())
                                    {
                                        numMatches++;
                                    }
                                }
                            }
                        }
                        if (kvp.Key == "experienceLevel")
                        {
                            var experienceLevels = kvp.Value.L;
                            int numExp = 0;
                            foreach (var e in experienceLevels)
                            {
                                string s = e.S;
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
                        results.Add(item["groupId"].S);
                    }
                }

                //FindAFam famSearch = new FindAFam(user, tagsList, experienceLevel);
                Intent intent = new Intent(this, typeof(EventMatchesActivity));
                //var results = famSearch.FamSearchResults;    
                intent.PutExtra("matches", results.ToArray());
                intent.PutExtra("userId", userId);
                StartActivity(intent);*/
                //StartActivity(typeof(EventMatchesActivity));
            };

            ImageButton imagebutton1 = FindViewById<ImageButton>(Resource.Id.imageButton1);
            imagebutton1.Click += delegate {
                StartActivity(typeof(HomepageActivity));
            };

            ImageButton imagebutton2 = FindViewById<ImageButton>(Resource.Id.imageButton2);
            imagebutton2.Click += delegate {
                Intent intent = new Intent(this, typeof(ProfilePageActivity));
                intent.PutExtra("userId", userId);
                intent.PutExtra("profileId", userId);
                intent.PutExtra("bio", user.Bio);
                intent.PutExtra("username", user.Username);
                intent.PutExtra("gender", user.Gender);
                //intent.Put("activities", user.Activities);
                StartActivity(intent);
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