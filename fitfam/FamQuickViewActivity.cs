using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace fitfam
{
    [Activity(Label = "FamQuickViewActivity")]
    public class FamQuickViewActivity : Activity
    {
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.FamQuickView);
            String groupId = Intent.GetStringExtra("groupId");
            var userId = Intent.GetStringExtra("userId");

            ImageButton imagebutton1 = FindViewById<ImageButton>(Resource.Id.imageButton1);
            imagebutton1.Click += delegate {
                Intent intent = new Intent(this, typeof(HomepageActivity));
                intent.PutExtra("userId", userId);
                StartActivity(intent);
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


            var request = new GetItemRequest
            {
                TableName = tableName,
                Key = new Dictionary<string, AttributeValue>() { { "groupId", new AttributeValue { S = groupId } } },
            };

            Dictionary<string, AttributeValue> groupInfo = new Dictionary<string, AttributeValue>();
            var task = await client.GetItemAsync(dbclient, request);
            groupInfo = task;

            //===============================STUFFFF=================================================
            try
            {
                LinearLayout layout = FindViewById<LinearLayout>(Resource.Id.linearLayout4);
                // Group Name
                TextView textView1 = FindViewById<TextView>(Resource.Id.textView1);
                textView1.SetTextColor(Color.Black);
                textView1.Text = groupInfo["groupName"].S;
                textView1.SetTextAppearance(this, Android.Resource.Style.TextAppearanceLarge);
                
                // Members Count
                TextView textView2 = FindViewById<TextView>(Resource.Id.textView2);
                textView2.SetTextColor(Color.Black);
                textView2.Text = ("Members: " + groupInfo["members"].M.Count.ToString());
                textView2.SetTextAppearance(this, Android.Resource.Style.TextAppearanceMedium);
                
                // Description
                TextView textView3 = FindViewById<TextView>(Resource.Id.textView3);
                textView3.SetTextColor(Color.Black);
                Console.WriteLine(groupInfo["description"]);
                textView3.Text = ("Description: " + groupInfo["description"].S);
                textView3.SetTextAppearance(this, Android.Resource.Style.TextAppearanceMedium);
                

                Button join = FindViewById<Button>(Resource.Id.button1);

            }
            catch (Exception ex)
            {
                System.Console.WriteLine("EXCEPTION: {0}\n{1}", ex.Message, ex.StackTrace);
            }
            //join.Click += OnAlertYesNoClicked;

            //async void OnAlertYesNoClicked(object sender, EventArgs e)
            //{
            //    Android.App.AlertDialog.Builder builder = new AlertDialog.Builder(this);
            //    AlertDialog alertDialog = builder.Create();
            //    alertDialog.SetTitle("Join Request");
            //    alertDialog.SetMessage("You are about to send a join request. Would you like to continue?");
            //    alertDialog.SetButton("No", (s, ev) =>
            //    {
            //        alertDialog.Cancel();
            //    });
            //    alertDialog.SetButton2("Yes", (s, ev) =>
            //    {

            //    });
            //    alertDialog.Show();
            //}






        }
    }
}
