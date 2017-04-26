using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

/*
 * FitFam 
 * 
 * Ehsan Ahmed, Jessa Marie Barre, Shannon Fisher, 
 * Josh Jacobson, Korey Prendergast, Chandrika Sinha
 * 4/20/2017
 * 
 * HomepageActivity: home page
 * users can create and find events and groups
 * 
 */

namespace fitfam
{
    [Activity(Label = "HomepageActivity")]
    public class HomepageActivity : Activity
    {
        // private member variables
        private User user;
        private string userId;
        private string gender;
        private string pic;
        private string location;
        private string username;

        // initialize UI and get user information from main page
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Homepage);

            // get information from previous page
            userId = Intent.GetStringExtra("userId") ?? "null";
            pic = Intent.GetStringExtra("pic") ?? "null";
            location = Intent.GetStringExtra("location") ?? "null";
            username = Intent.GetStringExtra("username") ?? "null";
            var genderInt = Intent.GetIntExtra("gender", -1);
            user = new User(userId, gender, pic, location, username);

            // set the gender
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


            // buttons to find and create groups and events
            Button findFamButton = FindViewById<Button>(Resource.Id.findFamButton);
            findFamButton.Click += delegate {
                Intent intent = new Intent(this, typeof(FindAFamFormActivity));
                intent.PutExtra("userId", userId);
                intent.PutExtra("profileId", userId);
                intent.PutExtra("pic", pic);
                intent.PutExtra("location", location);
                intent.PutExtra("username", username);
                intent.PutExtra("gender", user.Gender);
                StartActivity(intent);
            };

            Button createFamButton = FindViewById<Button>(Resource.Id.createFamButton);
            createFamButton.Click += delegate {
                Intent intent = new Intent(this, typeof(CreateAFamFormActivity));
                intent.PutExtra("userId", userId);
                intent.PutExtra("profileId", userId);
                intent.PutExtra("pic", pic);
                intent.PutExtra("location", location);
                intent.PutExtra("username", username);
                intent.PutExtra("gender", user.Gender);
                StartActivity(intent);
            };

            Button findEventButton = FindViewById<Button>(Resource.Id.findEventButton);
            findEventButton.Click += delegate {
                Intent intent = new Intent(this, typeof(FindAnEventActivity));
                intent.PutExtra("userId", userId);
                intent.PutExtra("profileId", userId);
                intent.PutExtra("pic", pic);
                intent.PutExtra("location", location);
                intent.PutExtra("username", username);
                intent.PutExtra("gender", user.Gender);
                StartActivity(intent);
            };

            Button createEventButton = FindViewById<Button>(Resource.Id.createEventButton);
            createEventButton.Click += delegate {
                Intent intent = new Intent(this, typeof(CreateEventActivity));
                intent.PutExtra("userId", userId);
                intent.PutExtra("profileId", userId);
                intent.PutExtra("pic", pic);
                intent.PutExtra("location", location);
                intent.PutExtra("username", username);
                intent.PutExtra("gender", user.Gender);
                StartActivity(intent);
            };
        }
    }
}
