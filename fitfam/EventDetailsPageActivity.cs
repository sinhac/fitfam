using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Android.App;
using Android.OS;
using Android.Widget;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace fitfam
{
    [Activity(Label = "Activity1")]
    public class EventDetailsPageActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.EventDetailsPage);


            string eventId = Intent.GetStringExtra("eventId") ?? "Data not available";

            System.Console.WriteLine("event" + eventId);

            //===========================STUFFFFFF====================================================

            AWSClient client = new AWSClient(Amazon.RegionEndpoint.USEast1);
            AmazonDynamoDBClient dbclient = client.getDynamoDBClient();
            string tableName = "fitfam-mobilehub-2083376203-events";


            var request = new GetItemRequest
            {
                TableName = tableName,
                Key = new Dictionary<string, AttributeValue>() { { "eventId", new AttributeValue { S = eventId } } },
            };
            //var key = new Dictionary<string, AttributeValue>() { { "eventId", new AttributeValue { S = eventId } } };
            //CancellationToken token;
            //var response = client.GetItemAsync(tableName, key, token);
            var response = client.getItemAsync(dbclient, request);
            System.Console.WriteLine("here"+response.ToString());
            System.Console.WriteLine("here" + response.HttpStatusCode);
            System.Console.WriteLine("here1" + response.ContentLength);
            System.Console.WriteLine("here2" + response.IsItemSet);


            // Check the response.
            var result = response.Item;
            //var attributeMap = result.Item; // Attribute list in the response.
            //var something = result.Values;

            List<KeyValuePair<string, AttributeValue>> info = new List<KeyValuePair<string, AttributeValue>>();
            if (result.Count == 0) { System.Console.WriteLine("EMPTYYYYYYYYYYYYYYYYYYY1111111"); }
            foreach (KeyValuePair<string, AttributeValue> entry in result)
            {
                info.Add(entry);
                System.Console.WriteLine("THIIIIIIIIIIIIIIIIIIING "+entry.Key);
                //System.Console.WriteLine("VALUUUUUUUUUUUUUUUUUUUUE" + (string)entry.Value.S);
                //System.Console.WriteLine("VALUUUUUUUUUUUUUUUUUUUUE");
            }

            /*foreach (KeyValuePair<string, AttributeValue> kvp in info)
            {
                System.Console.WriteLine("THIIIIIIIIIIIIIIIIIIING "+kvp.Key);
                System.Console.WriteLine("VALUUUUUUUUUUUUUUUUUUUUE" + (string)kvp.Value.S);
            }*/

            //string Value = "";
            //Value = string.Join(", ", result.Values.Select(x => x));
            /*Enumerable.Range(0, /*result.Values.Count*///3).Select(i =>
            //    Value = string.Join(",", result.Values.Select(x => x)) 
            //    );*/
            System.Console.WriteLine("Idddddddddddddddddddddddddddddddd: " + eventId);
            //System.Console.WriteLine("HEEEEEEEEEEEEEEEEEEEEEEYYYYYYYYYYYYYYYYYYYYY: " + Value);


            //===============================STUFFFF=================================================

            Button button1 = FindViewById<Button>(Resource.Id.button1);
            button1.Click += delegate {
                StartActivity(typeof(EventAttendeesActivity));
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

            LinearLayout layout = FindViewById<LinearLayout>(Resource.Id.linearLayout4);
            if (true)
            {
                Button button = new Button(this);
                button.Id = 3;
                button.Text = "Edit Event Details";
                button.SetBackgroundResource(Resource.Drawable.gold_button);
                float scale = button.Resources.DisplayMetrics.Density;
                button.SetHeight((int)(75 * scale + 0.5f));
                button.SetWidth((int)(500 * scale + 0.5f));
                int padding = (int)(16 * scale + 0.5f);
                button.SetPadding(padding, padding, padding, padding);
                layout.AddView(button);

                button.Click += delegate {
                    StartActivity(typeof(EditEventDetailsActivity));
                };
            }
        }
    }
}