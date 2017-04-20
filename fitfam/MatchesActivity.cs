using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Android.App;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Graphics;
using Android.Widget;
using System.Collections.Generic;
using static Android.Resource;
using System;

namespace fitfam
{
    [Activity(Label = "matchesActivity")]
    public class MatchesActivity : Activity
    {
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            string[] matches = Intent.GetStringArrayExtra("matches");
            // Create your application here
            SetContentView(Resource.Layout.Matches);

            Button backtosearch_button = FindViewById<Button>(Resource.Id.button1);

            backtosearch_button.Click += delegate {
                StartActivity(typeof(FindAFamFormActivity));
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

            Button button2 = FindViewById<Button>(Resource.Id.button1);
            button2.Click += delegate {
                StartActivity(typeof(FamQuickViewActivity));
            };
            
            //num_buttons will be taken from database COUNT(matches)
            int num_buttons = 10;

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

                // Group objects grabbed using groupIds in database
                AWSClient client = new AWSClient(Amazon.RegionEndpoint.USEast1);
                AmazonDynamoDBClient dbclient = client.getDynamoDBClient();
                string tableName = "fitfam-mobilehub-2083376203-groups";
                var request = new GetItemRequest
                {
                    TableName = tableName,
                    Key = new Dictionary<string, AttributeValue>() { { "groupId", new AttributeValue { S = matches[i] } } },
                };

                Dictionary<string, AttributeValue> groupInfo = new Dictionary<string, AttributeValue>();
                var task = await client.GetItemAsync(dbclient, request);
                groupInfo = task;
                try
                {
                    LinearLayout layout = FindViewById<LinearLayout>(Resource.Id.linearLayout4);
                    // Group Name
                    button.Text = groupInfo["groupName"].S;
                    button.SetTextColor(Android.Graphics.Color.Black);
                    matchButtonLayout.AddView(button);

                    Space sp = new Space(this);
                    sp.SetPadding(padding, padding, padding, padding);

                    matchButtonLayout.AddView(sp);

                    button.Click += delegate
                    {
                        StartActivity(typeof(FamQuickViewActivity));
                    };
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("EXCEPTION: {0}\n{1}", ex.Message, ex.StackTrace);
                }
            }
        }
    }
}
