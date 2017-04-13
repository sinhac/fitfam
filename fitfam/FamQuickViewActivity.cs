using Android.App;
using Android.OS;
using Android.Widget;
using System;

namespace fitfam
{
    [Activity(Label = "FamQuickViewActivity")]
    public class FamQuickViewActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.FamQuickView);

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

            Button join = FindViewById<Button>(Resource.Id.button1);
            join.Click += OnAlertYesNoClicked;

            async void OnAlertYesNoClicked(object sender, EventArgs e)
            {
                Android.App.AlertDialog.Builder builder = new AlertDialog.Builder(this);
                AlertDialog alertDialog = builder.Create();
                alertDialog.SetTitle("Join Request");
                alertDialog.SetMessage("You are about to send a join request. Would you like to continue?");
                alertDialog.SetButton("No", (s, ev) =>
                {
                    alertDialog.Cancel();
                });
                alertDialog.SetButton2("Yes", (s, ev) =>
                {
                    
                });
                alertDialog.Show();
            }
        }
    }
}
