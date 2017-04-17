using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Android.App;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace fitfam
{
    [Activity(Label = "CreateEventActivity")]
    public class CreateEventActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CreateEventPage);

            // capture user input for event name, location, description, tags
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

            // get tags
            var tags = FindViewById<MultiAutoCompleteTextView>(Resource.Id.multiAutoCompleteTextView3);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.ACTIVITIES, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            
            tags.Adapter = adapter;
            tags.SetTokenizer(new MultiAutoCompleteTextView.CommaTokenizer());

            var tagsInput = "";
            string[] tagsArr;
            List<string> tagsList = new List<string>();
            tags.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                tagsInput = e.Text.ToString();
            };
            char[] delimiters = { ',', '\t', '\n' };
            tagsArr = tagsInput.Split(delimiters);
            for(int i = 0; i < tagsArr.Length; i++)
            {
                tagsList.Add(tagsArr[i]);
            }

            // add user input to new entry in database, then redirect 
            Button button1 = FindViewById<Button>(Resource.Id.button1);
            button1.Click += delegate {
                Event newEvent = new Event(eventNameInput, descriptionInput, locationInput, default(DateTime), default(DateTime), true, tagsList,new fitfam.User("fakeCreator"));
                // =============================  START TEST -- REMOVE  ===============================================
                AWSClient client = new AWSClient(Amazon.RegionEndpoint.USEast1);
                Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = client.getDynamoDBClient();
                var request = new GetItemRequest
                {
                    TableName = "fitfam-mobilehub-2083376203-events",
                    Key = new Dictionary<string, AttributeValue>()
                    {
                        { "eventId", new AttributeValue { S = eventNameInput + "fakeCreator" + default(DateTime).ToString() } }
                    },
                };
                var response = dbclient.GetItemAsync(request);
                GetItemResponse result = response.Result;
                var item = result.Item;

                System.Console.WriteLine("SUCCESS: NEW EVENT NAME = " + item[eventNameInput] + ", DESCRIPTION = " + item[descriptionInput] + ", LOCATION = " + item[locationInput] + "\n\n\n");
                // ================================ END TEST  =======================================================
                StartActivity(typeof(EventDetailsPageActivity));
            };

            /* navbar buttons */
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