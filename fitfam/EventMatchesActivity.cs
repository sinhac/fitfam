using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using static Android.Resource;

/*
 * FitFam 
 * 
 * Ehsan Ahmed, Jessa Marie Barre, Shannon Fisher, 
 * Josh Jacobson, Korey Prendergast, Chandrika Sinha
 * 4/20/2017
 * 
 * EventMatchesActivity: Shows the events that have been found from search
 * 
 */

namespace fitfam
{
    [Activity(Label = "EventMatchesActivity")]
    public class EventMatchesActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EventMatches);

            string eventId = Intent.GetStringExtra("eventId") ?? "Data not available";
            string userId = Intent.GetStringExtra("userId") ?? "null";
            User newAttendingUser = new User(userId,false);
            newAttendingUser.attendingEvent(new Event(eventId));

            string toast = string.Format("You have been added to {0}", eventId);
            Toast.MakeText(this, toast, ToastLength.Long).Show();

            Button backtosearch_button = FindViewById<Button>(Resource.Id.backToSearchButton);

            backtosearch_button.Click += delegate {
                StartActivity(typeof(FindAnEventActivity));
            };

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
                intent.PutExtra("bio", newAttendingUser.Bio);
                intent.PutExtra("username", newAttendingUser.Username);
                intent.PutExtra("gender", newAttendingUser.Gender);
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

            //num_buttons will be taken from database COUNT(matches)
            int num_buttons = 10;

            ViewGroup matchButtonLayout = (ViewGroup)FindViewById(Resource.Id.radioGroup1);  // This is the id of the RadioGroup we defined
            for (var i = 0; i < num_buttons; i++)
            {
                Button button = new Button(this);
                //Does not change
                button.Id = (i);
                button.SetBackgroundResource(Resource.Drawable.gold_button); // This is a custom button drawable, defined in XML 
                float scale = button.Resources.DisplayMetrics.Density;
                button.SetHeight((int)(75 * scale + 0.5f));
                button.SetWidth((int)(500 * scale + 0.5f));
                int padding = (int)(16 * scale + 0.5f);
                button.SetPadding(padding, padding, padding, padding);
                Android.Graphics.Drawables.Drawable drawable = Resources.GetDrawable(Resource.Drawable.miniMightyMan);
                drawable.SetBounds(0, 0, (int)(drawable.IntrinsicWidth * 0.5), (int)(drawable.IntrinsicHeight * 0.5));
                Android.Graphics.Drawables.ScaleDrawable sd = new Android.Graphics.Drawables.ScaleDrawable(drawable, 0, (int)(drawable.IntrinsicWidth * scale + 0.5f), (int)(drawable.IntrinsicHeight * scale + 0.5f));
                button.SetCompoundDrawables(sd.Drawable, null, null, null);

                //Should get fed in from database
                button.Text = "SS Quarter Finals";

                matchButtonLayout.AddView(button);

                Space sp = new Space(this);
                sp.SetPadding(padding, padding, padding, padding);

                matchButtonLayout.AddView(sp);

                button.Click += delegate {
                    StartActivity(typeof(EventDetailsPageActivity));
                };
            };


        }
    }
}
