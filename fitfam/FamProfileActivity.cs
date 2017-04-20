using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace fitfam
{
    [Activity(Label = "Activity1")]
    public class FamProfileActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.FamProfile);
            var userId = Intent.GetStringExtra("userId");

            string groupId = Intent.GetStringExtra("groupId") ?? "Data not available";
            string userId = Intent.GetStringExtra("userId") ?? "Data not available";
            User user = new User(userId, true);

            Group g = new Group(groupId);
            string toast = string.Format("You have been added to {0}", g.GroupName.ToString());
            Toast.MakeText(this, toast, ToastLength.Long).Show();

            ImageButton imagebutton1 = FindViewById<ImageButton>(Resource.Id.imageButton1);
            imagebutton1.Click += delegate {
                Intent intent = new Intent(this, typeof(HomepageActivity));
                intent.PutExtra("userId", userId);
                StartActivity(intent);
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

            /*
            Button button1 = FindViewById<Button>(Resource.Id.button1);
            button1.Click += delegate {
                StartActivity(typeof(FamDetailsPageActivity));
            };*/

        }
    }
}
