using Android.App;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using static Android.Resource;

namespace fitfam
{
    [Activity(Label = "matchesActivity")]
    public class MatchesActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Matches);

            Button match_button = FindViewById<Button>(Resource.Id.button1);

            match_button.Click += delegate {
                StartActivity(typeof(FamQuickViewActivity));
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

            Button button2 = FindViewById<Button>(Resource.Id.button1);
            button2.Click += delegate {
                StartActivity(typeof(FamQuickViewActivity));
            };
            
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
                button.Text = "Soccer Squad";

                matchButtonLayout.AddView(button);

                Space sp = new Space(this);
                sp.SetPadding(padding, padding, padding, padding);

                matchButtonLayout.AddView(sp);

                button.Click += delegate {
                    StartActivity(typeof(FamQuickViewActivity));
                };
            };


        }
    }
}
