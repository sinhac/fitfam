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
        protected async override void OnCreate(Bundle savedInstanceState)
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
            //var task = client.GetItemAsync(dbclient, request);
            //GetItemResponse response = client.GetItemAsync(dbclient, request);

            //Dictionary<string, AttributeValue> eventInfo = await client.GetItemAsync(dbclient, request);
            Dictionary<string, AttributeValue> eventInfo = new Dictionary<string, AttributeValue>();
            if (eventInfo.Count == 0)
            {
                System.Console.WriteLine("NOOOOOOOOOOOOOOOOOO KEEEEEEEEEEEEEEEEEEEEEEEEEEEEEYYYYYYYYYYYYYYYS111111111111");
            }
            var task = await client.GetItemAsync(dbclient, request);
            eventInfo = task;
           
            if (task.Values.Count > 0)
            {
                System.Console.WriteLine("SUCCESSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS");
            }

            string eventNameInput = "";
            foreach (KeyValuePair<string, AttributeValue> kvp in eventInfo)
            {
                System.Console.WriteLine("THIIIIIIIIIIIIIIIIIIING " + kvp.Key);
                //System.Console.WriteLine("VALUUUUUUUUUUUUUUUUUUUUE" + kvp.Value);
                var value = kvp.Value;
                eventNameInput = value.S;
                System.Console.WriteLine(
                    "VALUUUUE: "+kvp.Key + " " +
                    (value.S == null ? "" : "S=[" + value.S + "]") +
                    (value.N == null ? "" : "N=[" + value.N + "]") +
                    (value.SS == null ? "" : "SS=[" + string.Join(",", value.SS.ToArray()) + "]") +
                    (value.NS == null ? "" : "NS=[" + string.Join(",", value.NS.ToArray()) + "]")
                    );
            }

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