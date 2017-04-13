using Android.App;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace fitfam
{
    [Activity(Label = "CreateEventActivity")]
    public class CreateEventActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CreateEventPage);

            // Create your application here
            var eventName = FindViewById<MultiAutoCompleteTextView>(Resource.Id.multiAutoCompleteTextView1);
            var eventNameInput = "";
            eventName.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                eventNameInput = e.Text.ToString();
            };

            var location = FindViewById<MultiAutoCompleteTextView>(Resource.Id.multiAutoCompleteTextView2);
            var locationInput = "";
            location.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                locationInput = e.Text.ToString();
            };

            var description = FindViewById<EditText>(Resource.Id.editText1);
            var descriptionInput = "";
            description.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                descriptionInput = e.Text.ToString();
            };

            //ADD TAGS HERE

            // Create your application here
            Button button1 = FindViewById<Button>(Resource.Id.button1);

            button1.Click += delegate {
                Event newEvent = new Event(eventNameInput, descriptionInput, locationInput, default(DateTime), default(DateTime), true, new List<string>(),new fitfam.User("fakeCreator"));
                StartActivity(typeof(EventDetailsPageActivity));
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
            
        }
    }
}