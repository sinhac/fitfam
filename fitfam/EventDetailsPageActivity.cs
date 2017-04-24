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
    [Activity(Label = "Event Details")]
    public class EventDetailsPageActivity : Activity
    {
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EventDetailsPage);
         
            // get information from previous page
            string userId = Intent.GetStringExtra("userId") ?? "null";
            User user = new User(userId, false);
            var pic = Intent.GetStringExtra("pic") ?? "null";
            var location = Intent.GetStringExtra("location") ?? "null";
            var username = Intent.GetStringExtra("username") ?? "null";
            var genderInt = Intent.GetIntExtra("gender", -1);
            var profileId = Intent.GetStringExtra("profileId") ?? "Null";
            string eventId = Intent.GetStringExtra("eventId") ?? "Data not available";

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

            AWSClient client = new AWSClient(Amazon.RegionEndpoint.USEast1);
            AmazonDynamoDBClient dbclient = client.getDynamoDBClient();
            string tableName = "fitfam-mobilehub-2083376203-events";

            var request = new GetItemRequest
            {
                TableName = tableName,
                Key = new Dictionary<string, AttributeValue>() { { "eventId", new AttributeValue { S = eventId } } },
            };

            Dictionary<string, AttributeValue> eventInfo = new Dictionary<string, AttributeValue>();
            var task = await client.GetItemAsync(dbclient, request);
            eventInfo = task;

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

                Button attendeesButton = new Button(this);
                attendeesButton.Id = 4;
                attendeesButton.Text = "Attendees";
                attendeesButton.SetTextColor(Color.Black);
                attendeesButton.SetBackgroundResource(Resource.Drawable.gold_button);
                float scale = attendeesButton.Resources.DisplayMetrics.Density;
                attendeesButton.SetHeight((int)(75 * scale + 0.5f));
                attendeesButton.SetWidth((int)(500 * scale + 0.5f));
                int padding = (int)(16 * scale + 0.5f);
                attendeesButton.SetPadding(padding, padding, padding, padding);
                layout.AddView(attendeesButton);

                attendeesButton.Click += delegate {
                    Intent intent = new Intent(this, typeof(EventAttendeesActivity));
                    intent.PutExtra("eventId", eventId);
                    intent.PutExtra("userId", userId);
                    intent.PutExtra("profileId", userId);
                    intent.PutExtra("pic", pic);
                    intent.PutExtra("location", location);
                    intent.PutExtra("username", username);
                    intent.PutExtra("gender", genderInt);
                    StartActivity(intent);
                };

            Button joinButton = new Button(this);
            joinButton.Id = 5;
            //joinButton.Text = "Join "+eventInfo["eventName"].S;
            joinButton.Text = "join event";
            joinButton.SetTextColor(Color.Black);
            joinButton.SetBackgroundResource(Resource.Drawable.gold_button);
            //float scale = joinButton.Resources.DisplayMetrics.Density;
            joinButton.SetHeight((int)(75 * scale + 0.5f));
            joinButton.SetWidth((int)(500 * scale + 0.5f));
            //int padding = (int)(16 * scale + 0.5f);
            joinButton.SetPadding(padding, padding, padding, padding);
            layout.AddView(joinButton);

            joinButton.Click += delegate {
                var eventMatchesActivity = new Intent(this, typeof(EventMatchesActivity));
                eventMatchesActivity.PutExtra("eventId", eventId);
                eventMatchesActivity.PutExtra("userId", userId);
                StartActivity(eventMatchesActivity);
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