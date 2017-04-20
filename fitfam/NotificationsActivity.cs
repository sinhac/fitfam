using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;

namespace fitfam
{
    [Activity(Label = "NotificationActivity")]
    public class NotificationsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Notifications);

            // navbar buttons

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

            //num_buttons will be taken from database COUNT(notifications)
            int num_buttons = 10;

            ViewGroup notificationLayout = (ViewGroup)FindViewById(Resource.Id.radioGroup1);  // This is the id of the RadioGroup we defined
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
                button.Text = "mightman11";

                notificationLayout.AddView(button);

                Space sp = new Space(this);
                sp.SetPadding(padding, padding, padding, padding);

                notificationLayout.AddView(sp);

                //button.Click += OnAlertYesNoClicked;
            };

            //async void OnAlertYesNoClicked(object sender, EventArgs e)
            //{
            //    Android.App.AlertDialog.Builder builder = new AlertDialog.Builder(this);
            //    AlertDialog alertDialog = builder.Create();
            //    alertDialog.SetTitle("Join Request");
            //    alertDialog.SetMessage("This user would like to join your fam. What would you like to do?");
            //    alertDialog.SetButton("Decline", (s, ev) =>
            //    {
            //        alertDialog.Cancel();
            //        //decline
            //    });
            //    alertDialog.SetButton2("Accept", (s, ev) =>
            //    {
            //        //accept
            //    });
            //    alertDialog.SetButton3("View Profile", (s, ev) =>
            //    {
            //        StartActivity(typeof(ProfilePageActivity));
            //    });
            //    alertDialog.Show();
            //}
        }
    }
}