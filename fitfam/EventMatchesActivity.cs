using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using static Android.Resource;

/*
 * FitFam 
 * 
 * Ehsan Ahmed, Jessa Marie Barre, Shannon Fisher, 
 * Josh Jacobson, Korey Prendergast, Chandrika Sinha
 * 4/20/2017
 * 
 * EventMatchesActivity: Shows the events that have been found from search
 * 
 */

namespace fitfam
{
    [Activity(Label = "EventMatchesActivity")]
    public class EventMatchesActivity : Activity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EventMatches);

            // get information from previous page
            string[] matches = Intent.GetStringArrayExtra("matches");
            string userId = Intent.GetStringExtra("userId") ?? "null";
            User user = new User(userId, false);
            var pic = Intent.GetStringExtra("pic") ?? "null";
            var location = Intent.GetStringExtra("location") ?? "null";
            var username = Intent.GetStringExtra("username") ?? "null";
            var genderInt = Intent.GetIntExtra("gender", -1);
            var profileId = Intent.GetStringExtra("profileId") ?? "Null";
            System.Console.WriteLine("Size of event matches: " + matches.Length + "  userId: " + userId + "  pic: " + pic + "  location: " + location + "  userName: " + username + "  gender: " + genderInt + "  profileId: " + profileId);

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

            //user.attendingEvent(new Event(eventId));

            //string toast = string.Format("You have been added to {0}", eventId);
            //Toast.MakeText(this, toast, ToastLength.Long).Show();

            Button backtosearch_button = FindViewById<Button>(Resource.Id.backToSearchButton);
            backtosearch_button.Click += delegate
            {
                Intent intent = new Intent(this, typeof(FindAnEventActivity));
                intent.PutExtra("userId", userId);
                intent.PutExtra("profileId", userId);
                intent.PutExtra("pic", pic);
                intent.PutExtra("location", location);
                intent.PutExtra("username", username);
                intent.PutExtra("gender", genderInt);
                StartActivity(intent);
            };


            //num_buttons will be taken from database COUNT(matches)
            int num_buttons = matches.Length;

            System.Console.WriteLine("MAAAAAAAAAAAAAAAAAAAATCHES " + matches.Length);
            ViewGroup matchButtonLayout = (ViewGroup)FindViewById(Resource.Id.radioGroup1);  // This is the id of the RadioGroup we defined
            for (var i = 0; i < matches.Length; i++)
            {
                Button button = new Button(this);
                //Does not change
                button.Id = (i);
                button.SetBackgroundResource(Resource.Drawable.gold_button); // This is a custom button drawable, defined in XML 
                float scale = button.Resources.DisplayMetrics.Density;
                button.SetHeight((int)(75 * scale + 0.5f));
                button.SetWidth((int)(500 * scale + 0.5f));
                int padding = (int)(16 * scale + 0.5f);
                button.SetPadding(padding, padding, padding, padding);
                Android.Graphics.Drawables.Drawable drawable = Resources.GetDrawable(Resource.Drawable.miniMightyMan);
                drawable.SetBounds(0, 0, (int)(drawable.IntrinsicWidth * 0.5), (int)(drawable.IntrinsicHeight * 0.5));
                Android.Graphics.Drawables.ScaleDrawable sd = new Android.Graphics.Drawables.ScaleDrawable(drawable, 0, (int)(drawable.IntrinsicWidth * scale + 0.5f), (int)(drawable.IntrinsicHeight * scale + 0.5f));
                button.SetCompoundDrawables(sd.Drawable, null, null, null);

                // Event objects grabbed using eventIds in database
                AWSClient client = new AWSClient(Amazon.RegionEndpoint.USEast1);
                AmazonDynamoDBClient dbclient = client.getDynamoDBClient();
                string tableName = "fitfam-mobilehub-2083376203-events";
                var request = new GetItemRequest
                {
                    TableName = tableName,
                    Key = new Dictionary<string, AttributeValue>() { { "eventId", new AttributeValue { S = matches[i] } } },
                };
                System.Console.WriteLine("but did I make it this far?");
                Dictionary<string, AttributeValue> eventInfo = new Dictionary<string, AttributeValue>();
                var task = await client.GetItemAsync(dbclient, request);
                eventInfo = task;
                try
                {
                    LinearLayout layout = FindViewById<LinearLayout>(Resource.Id.linearLayout4);
                    // display event name in button
                    button.Text = eventInfo["eventName"].S;
                    button.SetTextColor(Android.Graphics.Color.Black);
                    matchButtonLayout.AddView(button);

                    Space sp = new Space(this);
                    sp.SetPadding(padding, padding, padding, padding);

                    matchButtonLayout.AddView(sp);

                    button.Click += delegate
                    {
                        Intent intent = new Intent(this, typeof(EventDetailsPageActivity));
                        intent.PutExtra("eventId", eventInfo["eventId"].S);
                        intent.PutExtra("userId", userId);
                        intent.PutExtra("myEvent", false);
                        intent.PutExtra("profileId", userId);
                        intent.PutExtra("pic", pic);
                        intent.PutExtra("location", location);
                        intent.PutExtra("username", username);
                        intent.PutExtra("gender", genderInt);
                        StartActivity(intent);
                    };
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("SO IT NEVER ENTERED TRY :((");
                    System.Console.WriteLine("EXCEPTION: {0}\n{1}", ex.Message, ex.StackTrace);
                }
            }

            ////num_buttons will be taken from database COUNT(matches)
            //int num_buttons = 10;

            //ViewGroup matchButtonLayout = (ViewGroup)FindViewById(Resource.Id.radioGroup1);  // This is the id of the RadioGroup we defined
            //for (var i = 0; i < num_buttons; i++)
            //{
            //    Button button = new Button(this);
            //    //Does not change
            //    button.Id = (i);
            //    button.SetBackgroundResource(Resource.Drawable.gold_button); // This is a custom button drawable, defined in XML 
            //    float scale = button.Resources.DisplayMetrics.Density;
            //    button.SetHeight((int)(75 * scale + 0.5f));
            //    button.SetWidth((int)(500 * scale + 0.5f));
            //    int padding = (int)(16 * scale + 0.5f);
            //    button.SetPadding(padding, padding, padding, padding);
            //    Android.Graphics.Drawables.Drawable drawable = Resources.GetDrawable(Resource.Drawable.miniMightyMan);
            //    drawable.SetBounds(0, 0, (int)(drawable.IntrinsicWidth * 0.5), (int)(drawable.IntrinsicHeight * 0.5));
            //    Android.Graphics.Drawables.ScaleDrawable sd = new Android.Graphics.Drawables.ScaleDrawable(drawable, 0, (int)(drawable.IntrinsicWidth * scale + 0.5f), (int)(drawable.IntrinsicHeight * scale + 0.5f));
            //    button.SetCompoundDrawables(sd.Drawable, null, null, null);

            //    //Should get fed in from database
            //    button.Text = "SS Quarter Finals";

            //    matchButtonLayout.AddView(button);

            //    Space sp = new Space(this);
            //    sp.SetPadding(padding, padding, padding, padding);

            //    matchButtonLayout.AddView(sp);

            //    button.Click += delegate {
            //        StartActivity(typeof(EventDetailsPageActivity));
            //    };
            //};


        }
    }
}
