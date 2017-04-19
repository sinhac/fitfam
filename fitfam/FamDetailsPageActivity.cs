using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace fitfam
{
    [Activity(Label = "FamDetailsPageActivity")]
    public class FamDetailsPageActivity : Activity
    {
        protected async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            string famId = Intent.GetStringExtra("famId") ?? "Data not available";


            // Create your application here
            SetContentView(Resource.Layout.FamDetailsPage);

            AWSClient client = new AWSClient(Amazon.RegionEndpoint.USEast1);
            AmazonDynamoDBClient dbclient = client.getDynamoDBClient();
            string tableName = "fitfam-mobilehub-2083376203-groups";

            var request = new GetItemRequest
            {
                TableName = tableName,
                Key = new Dictionary<string, AttributeValue>() { { "groupId", new AttributeValue { S = famId } } },
            };

            Dictionary<string, AttributeValue> famInfo = new Dictionary<string, AttributeValue>();
            if (famInfo.Count == 0)
            {
                System.Console.WriteLine("No fam info");
            }
            famInfo = await client.GetItemAsync(dbclient, request);

            if (famInfo.Values.Count > 0)
            {
                System.Console.WriteLine("Successfully created fam");
            }

            string famNameInput = "";
            foreach (KeyValuePair<string, AttributeValue> kvp in famInfo)
            {
                System.Console.WriteLine("THIIIIIIIIIIIIIIIIIIING " + kvp.Key);
                //System.Console.WriteLine("VALUUUUUUUUUUUUUUUUUUUUE" + kvp.Value);
                var value = kvp.Value;
                //eventNameInput = value.S;
                System.Console.WriteLine(
                    "VALUUUUE: " + kvp.Key + " " +
                    (value.S == null ? "" : "S=[" + value.S + "]") +
                    (value.N == null ? "" : "N=[" + value.N + "]") +
                    (value.SS == null ? "" : "SS=[" + string.Join(",", value.SS.ToArray()) + "]") +
                    (value.NS == null ? "" : "NS=[" + string.Join(",", value.NS.ToArray()) + "]")
                    );
            }

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

            Button button1 = FindViewById<Button>(Resource.Id.button1);
            button1.Click += delegate {
                StartActivity(typeof(EventPageActivity));
            };

            Button button2 = FindViewById<Button>(Resource.Id.button2);
            button2.Click += delegate {
                StartActivity(typeof(EventAttendeesActivity));
            };

            LinearLayout layout = FindViewById<LinearLayout>(Resource.Id.linearLayout4);
            if (true)
            {
                Button button = new Button(this);
                button.Id = 3;
                button.Text = "Edit Fam Details";
                button.SetBackgroundResource(Resource.Drawable.gold_button);
                float scale = button.Resources.DisplayMetrics.Density;
                button.SetHeight((int) (75 * scale + 0.5f));
                button.SetWidth((int) (500 * scale + 0.5f));
                int padding = (int)(16 * scale + 0.5f);
                button.SetPadding(padding, padding, padding, padding);
                layout.AddView(button);

                button.Click += delegate {
                    StartActivity(typeof(EditFamDetailsActivity));
                };
            }
        }
    }
}