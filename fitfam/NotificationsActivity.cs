using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

/*
 * FitFam 
 * 
 * Ehsan Ahmed, Jessa Marie Barre, Shannon Fisher, 
 * Josh Jacobson, Korey Prendergast, Chandrika Sinha
 * 4/20/2017
 * 
 * NotificationsActivity: The notifications page
 * Notifies user when new groups or event actions are happening
 * 
 */

namespace fitfam
{
    [Activity(Label = "NotificationActivity")]
    public class NotificationsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Notifications);

            // get information from previous page
            var userId = Intent.GetStringExtra("userId") ?? "null";
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

            //num_buttons will be taken from database COUNT(notifications)
            int num_buttons = 10;

            // This is the id of the RadioGroup we defined
            ViewGroup notificationLayout = (ViewGroup)FindViewById(Resource.Id.radioGroup1);  
            for (var i = 0; i < num_buttons; i++)
            {
                Button notificationButton = new Button(this);
                notificationButton.Id = (i);
                notificationButton.SetBackgroundResource(Resource.Drawable.gold_button); 
                float scale = notificationButton.Resources.DisplayMetrics.Density;
                notificationButton.SetHeight((int)(75 * scale + 0.5f));
                notificationButton.SetWidth((int)(500 * scale + 0.5f));
                int padding = (int)(16 * scale + 0.5f);
                notificationButton.SetPadding(padding, padding, padding, padding);
                Android.Graphics.Drawables.Drawable drawable = Resources.GetDrawable(Resource.Drawable.miniMightyMan);
                drawable.SetBounds(0, 0, (int)(drawable.IntrinsicWidth * 0.5), (int)(drawable.IntrinsicHeight * 0.5));
                Android.Graphics.Drawables.ScaleDrawable sd = new Android.Graphics.Drawables.ScaleDrawable(drawable, 0, (int)(drawable.IntrinsicWidth * scale + 0.5f), (int)(drawable.IntrinsicHeight * scale + 0.5f));
                notificationButton.SetCompoundDrawables(sd.Drawable, null, null, null);

                //Should get fed in from database
                notificationButton.Text = "mightyman11";
                notificationLayout.AddView(notificationButton);

                Space sp = new Space(this);
                sp.SetPadding(padding, padding, padding, padding);
                notificationLayout.AddView(sp);
            };
        }
    }
}