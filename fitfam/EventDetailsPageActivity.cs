using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using System;
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

            foreach (KeyValuePair<string, AttributeValue> kvp in eventInfo)
            {
                System.Console.WriteLine("THIIIIIIIIIIIIIIIIIIING " + kvp.Key);
                var value = kvp.Value;

                System.Console.WriteLine(
                    "VALUUUUE: " + kvp.Key + " " +
                    (value.S == null ? "" : "S=[" + value.S + "]") +
                    (value.N == null ? "" : "N=[" + value.N + "]") +
                    (value.SS == null ? "" : "SS=[" + string.Join(",", value.SS.ToArray()) + "]") +
                    (value.NS == null ? "" : "NS=[" + string.Join(",", value.NS.ToArray()) + "]")
                    );
            }

            System.Console.WriteLine("Idddddddddddddddddddddddddddddddd: " + eventId);


            //===============================STUFFFF=================================================
            try
            {
                LinearLayout layout = FindViewById<LinearLayout>(Resource.Id.linearLayout4);
                // Event Name
                TextView textView1 = new TextView(this);
                textView1.Text = eventInfo["eventName"].S;
                textView1.SetTextAppearance(this, Android.Resource.Style.TextAppearanceLarge);
                layout.AddView(textView1);

                // Event Location
                TextView textView2 = new TextView(this);
                textView2.Text = ("Location: " + eventInfo["location"].S);
                textView2.SetTextAppearance(this, Android.Resource.Style.TextAppearanceMedium);
                layout.AddView(textView2);
                // Description
                TextView textView3 = new TextView(this);
                textView3.Text = ("Description: " + eventInfo["description"].S);
                textView3.SetTextAppearance(this, Android.Resource.Style.TextAppearanceMedium);
                layout.AddView(textView3);
                // Start time
                TextView textView4 = new TextView(this);
                textView4.Text = ("Start Time: " + eventInfo["startTime"].S);
                textView4.SetTextAppearance(this, Android.Resource.Style.TextAppearanceMedium);
                layout.AddView(textView4);
                // End time
                TextView textView5 = new TextView(this);
                textView5.Text = ("End Time: " + eventInfo["endTime"].S);
                textView5.SetTextAppearance(this, Android.Resource.Style.TextAppearanceMedium);
                layout.AddView(textView5);

                Button button4 = new Button(this);
                button4.Id = 4;
                button4.Text = "Attendees";
                button4.SetTextColor(Color.Black);
                button4.SetBackgroundResource(Resource.Drawable.gold_button);
                float scale = button4.Resources.DisplayMetrics.Density;
                button4.SetHeight((int)(75 * scale + 0.5f));
                button4.SetWidth((int)(500 * scale + 0.5f));
                int padding = (int)(16 * scale + 0.5f);
                button4.SetPadding(padding, padding, padding, padding);
                layout.AddView(button4);

                button4.Click += delegate {
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

                // LinearLayout layout = FindViewById<LinearLayout>(Resource.Id.linearLayout4);
                if (true)
                {
                    Button button = new Button(this);
                    button.Id = 3;
                    button.Text = "Edit Event Details";
                    button.SetTextColor(Color.Black);
                    button.SetBackgroundResource(Resource.Drawable.gold_button);
                    // float scale = button.Resources.DisplayMetrics.Density;
                    button.SetHeight((int)(75 * scale + 0.5f));
                    button.SetWidth((int)(500 * scale + 0.5f));
                    // int padding = (int)(16 * scale + 0.5f);
                    button.SetPadding(padding, padding, padding, padding);
                    layout.AddView(button);

                    button.Click += delegate {
                        StartActivity(typeof(EditEventDetailsActivity));
                    };
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("EXCEPTION: {0}\n{1}", ex.Message, ex.StackTrace);
            }

            
        }
    }
}