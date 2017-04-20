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

namespace fitfam
{
    [Activity(Label = "EditFamDetailsActivity")]
    public class EditFamDetailsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EditFamDetails);
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

            string groupId = Intent.GetStringExtra("groupId") ?? "Data not available";
            EditText description = FindViewById<EditText>(Resource.Id.editText);
            var new_description = "";
            description.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                new_description = e.Text.ToString();
            };

            Button savechanges_button = FindViewById<Button>(Resource.Id.save_changes_button);
            savechanges_button.Click += delegate
            {
                //Group.editDescription(groupId, new_description);
                StartActivity(typeof(FamDetailsPageActivity));
            };
        }
    }
}