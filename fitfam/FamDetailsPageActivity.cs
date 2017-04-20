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
using Android.Graphics;

namespace fitfam
{
    [Activity(Label = "FamDetailsPageActivity")]
    public class FamDetailsPageActivity : Activity
    {
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.FamDetailsPage);

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

            AWSClient client = new AWSClient(Amazon.RegionEndpoint.USEast1);
            AmazonDynamoDBClient dbclient = client.getDynamoDBClient();
            string tableName = "fitfam-mobilehub-2083376203-groups";
            string groupId = Intent.GetStringExtra("groupId") ?? "Data not available";
            string userId = Intent.GetStringExtra("userId") ?? "null";
            var request = new GetItemRequest
            {
                TableName = tableName,
                Key = new Dictionary<string, AttributeValue>() { { "groupId", new AttributeValue { S = groupId } } },
            };

            Dictionary<string, AttributeValue> groupInfo = new Dictionary<string, AttributeValue>();
            var task = await client.GetItemAsync(dbclient, request);
            groupInfo = task;
            List<string> groupInformation = new List<string>();
            foreach (KeyValuePair<string, AttributeValue> kvp in groupInfo)
            {
                var value = kvp.Value;
                if (value.S == null)
                {

                }
                else
                {
                    groupInformation.Add(value.S);
                }
                if (value.N == null) { } else { }
                if (value.SS == null) { } else { }
                if (value.NS == null) { } else { }
            }

            //===============================STUFFFF=================================================

            LinearLayout layout = FindViewById<LinearLayout>(Resource.Id.linearLayout4);
            // Group ID
            // Group Name
            TextView textView1 = new TextView(this);
            textView1.Text = groupInfo["groupName"].S;
            textView1.SetTextAppearance(this, Android.Resource.Style.TextAppearanceLarge);
            layout.AddView(textView1);
            // Group Description
            TextView textView2 = new TextView(this);
            textView2.Text = ("Description: " + groupInfo["description"].S);
            textView2.SetTextAppearance(this, Android.Resource.Style.TextAppearanceMedium);
            layout.AddView(textView2);
            if (groupInformation.Count > 3)
            {
                //should at some point implement members and events
            }

            Button button1 = new Button(this);
            button1.Id = 4;
            button1.Text = "EVENTS";
            button1.SetTextColor(Color.Black);
            button1.SetBackgroundResource(Resource.Drawable.gold_button);
            float scale = button1.Resources.DisplayMetrics.Density;
            button1.SetHeight((int)(75 * scale + 0.5f));
            button1.SetWidth((int)(500 * scale + 0.5f));
            int padding = (int)(16 * scale + 0.5f);
            button1.SetPadding(padding, padding, padding, padding);
            layout.AddView(button1);

            button1.Click += delegate {
                StartActivity(typeof(EventPageActivity));
            };



            Button button2 = new Button(this);
            button2.Id = 4;
            button2.Text = "MEMBERS";
            button2.SetTextColor(Color.Black);
            button2.SetBackgroundResource(Resource.Drawable.gold_button);
            float scale2 = button2.Resources.DisplayMetrics.Density;
            button2.SetHeight((int)(75 * scale2 + 0.5f));
            button2.SetWidth((int)(500 * scale2 + 0.5f));
            int padding2 = (int)(16 * scale2 + 0.5f);
            button2.SetPadding(padding, padding, padding, padding);
            layout.AddView(button2);

            button2.Click += delegate {
                StartActivity(typeof(EventAttendeesActivity));
            };

            Button joinFamButton = new Button(this);
            joinFamButton.Id = 5;
            joinFamButton.Text = ("Join "+ groupInfo["groupName"].S);
            joinFamButton.SetTextColor(Color.Black);
            joinFamButton.SetBackgroundResource(Resource.Drawable.gold_button);
            float scale3 = joinFamButton.Resources.DisplayMetrics.Density;
            joinFamButton.SetHeight((int)(75 * scale2 + 0.5f));
            joinFamButton.SetWidth((int)(500 * scale2 + 0.5f));
            int padding3 = (int)(16 * scale2 + 0.5f);
            joinFamButton.SetPadding(padding, padding, padding, padding);
            layout.AddView(joinFamButton);

            joinFamButton.Click += delegate {

                var famProfileActivity = new Intent(this, typeof(FamProfileActivity));
                famProfileActivity.PutExtra("groupId", groupId);
                Group g = new Group(groupId);
                g.addJoinRequest(new User(userId, false));
                StartActivity(famProfileActivity);
            };




            // LinearLayout layout = FindViewById<LinearLayout>(Resource.Id.linearLayout4);
            if (true)
            {
                Button button = new Button(this);
                button.Id = 3;
                button.Text = "Edit Fam Details";
                button.SetBackgroundResource(Resource.Drawable.gold_button);
                // float scale = button.Resources.DisplayMetrics.Density;
                button.SetHeight((int) (75 * scale + 0.5f));
                button.SetWidth((int) (500 * scale + 0.5f));
                // int padding = (int)(16 * scale + 0.5f);
                button.SetPadding(padding, padding, padding, padding);
                layout.AddView(button);

                button.Click += delegate {
                    var intent = new Intent(this, typeof(EditFamDetailsActivity));
                    intent.PutExtra("groupId", groupId);
                    StartActivity(intent);
                };
            }
        }
    }
}