using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;

namespace fitfam
{
    [Activity(Label = "HomepageActivity")]
    public class HomepageActivity : Activity
    {
        private User user;
        private string userId;
        private string gender;
        private string pic;
        private string location;
        private string username;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            userId = Intent.GetStringExtra("userId") ?? "null";
            Console.WriteLine("home userId {0}", userId);
            var genderInt = Intent.GetIntExtra("gender", -1);
            switch (genderInt)
            {
                case 0:
                    gender = "MALE";
                    break;
                case 1:
                    gender = "FEMALE";
                    break;
                case 2:
                    gender = "OTHER";
                    break;
                default:
                    gender = "Null";
                    break;
            }

            pic = Intent.GetStringExtra("pic") ?? "null";
            location = Intent.GetStringExtra("location") ?? "null";
            username = Intent.GetStringExtra("username") ?? "null";
            user = new User(userId, gender, pic, location, username);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Homepage);

            Button find_fam_button = FindViewById<Button>(Resource.Id.find_fam_button);
            Button create_fam_button = FindViewById<Button>(Resource.Id.create_fam_button);
            Button find_event_button = FindViewById<Button>(Resource.Id.find_event_button);
            Button create_event_button = FindViewById<Button>(Resource.Id.create_event_button);

            /* redirect user according to button clicked */

            find_fam_button.Click += delegate {
                Intent intent = new Intent(this, typeof(FindAFamFormActivity));
                intent.PutExtra("userId", userId);
                StartActivity(intent);
            };

            create_fam_button.Click += delegate {
                Intent intent = new Intent(this, typeof(CreateAFamFormActivity));
                intent.PutExtra("userId", userId);
                StartActivity(intent);
            };

            find_event_button.Click += delegate {
                Intent intent = new Intent(this, typeof(FindAnEventActivity));
                intent.PutExtra("userId", userId);
                StartActivity(intent);
            };

            create_event_button.Click += delegate {
                Intent intent = new Intent(this, typeof(CreateEventActivity));
                intent.PutExtra("userId", userId);
                StartActivity(intent);
            };

            /* navbar buttons */
            ImageButton imagebutton1 = FindViewById<ImageButton>(Resource.Id.imageButton1);
            imagebutton1.Click += delegate {
                StartActivity(typeof(HomepageActivity));
            };

            ImageButton imagebutton2 = FindViewById<ImageButton>(Resource.Id.imageButton2);
            imagebutton2.Click += delegate {
                Intent intent = new Intent(this, typeof(ProfilePageActivity));
                intent.PutExtra("userId", userId);
                intent.PutExtra("profileId", userId);
                intent.PutExtra("bio", user.Bio);
                intent.PutExtra("username", user.Username);
                intent.PutExtra("gender", user.Gender);
                //intent.Put("activities", user.Activities);
                StartActivity(intent);
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
