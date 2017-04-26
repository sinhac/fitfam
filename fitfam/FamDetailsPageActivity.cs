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

/*
 * FitFam 
 * 
 * Ehsan Ahmed, Jessa Marie Barre, Shannon Fisher, 
 * Josh Jacobson, Korey Prendergast, Chandrika Sinha
 * 4/20/2017
 * 
 * FamDetailsPageActivity: the page that shows details about the group
 * 
 */

namespace fitfam
{
    [Activity(Label = "FamDetailsPageActivity")]
    public class FamDetailsPageActivity : Activity
    {
        private string userId;
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FamDetailsPage);

            // get information from previous page
            string userId = Intent.GetStringExtra("userId") ?? "null";
            User user = new User(userId, false);
            var pic = Intent.GetStringExtra("pic") ?? "null";
            var location = Intent.GetStringExtra("location") ?? "null";
            var username = Intent.GetStringExtra("username") ?? "null";
            var genderInt = Intent.GetIntExtra("gender", -1);
            var profileId = Intent.GetStringExtra("profileId") ?? "Null";

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
            string tableName = "fitfam-mobilehub-2083376203-groups";
            string groupId = Intent.GetStringExtra("groupId") ?? "Data not available";
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

            Button eventButton = new Button(this);
            eventButton.Id = 4;
            eventButton.Text = "EVENTS";
            eventButton.SetTextColor(Color.Black);
            eventButton.SetBackgroundResource(Resource.Drawable.gold_button);
            float scale = eventButton.Resources.DisplayMetrics.Density;
            eventButton.SetHeight((int)(75 * scale + 0.5f));
            eventButton.SetWidth((int)(500 * scale + 0.5f));
            int padding = (int)(16 * scale + 0.5f);
            eventButton.SetPadding(padding, padding, padding, padding);
            layout.AddView(eventButton);

            eventButton.Click += delegate {
                StartActivity(typeof(EventPageActivity));
            };



            Button membersButton = new Button(this);
            membersButton.Id = 4;
            membersButton.Text = "MEMBERS";
            membersButton.SetTextColor(Color.Black);
            membersButton.SetBackgroundResource(Resource.Drawable.gold_button);
            float scale2 = membersButton.Resources.DisplayMetrics.Density;
            membersButton.SetHeight((int)(75 * scale2 + 0.5f));
            membersButton.SetWidth((int)(500 * scale2 + 0.5f));
            int padding2 = (int)(16 * scale2 + 0.5f);
            membersButton.SetPadding(padding, padding, padding, padding);
            layout.AddView(membersButton);

            membersButton.Click += delegate {
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
            if (true)   // should only be enabled if user is group creator
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