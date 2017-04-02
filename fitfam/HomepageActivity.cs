using Android.App;
using Android.OS;
using Android.Widget;

namespace fitfam
{
    [Activity(Label = "HomepageActivity")]
    public class HomepageActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Homepage);

            // Create your application here
            Button find_fam_button = FindViewById<Button>(Resource.Id.find_fam_button);
            Button create_fam_button = FindViewById<Button>(Resource.Id.create_fam_button);
            Button find_event_button = FindViewById<Button>(Resource.Id.find_event_button);
            Button create_event_button = FindViewById<Button>(Resource.Id.create_event_button);

            find_fam_button.Click += delegate {
                StartActivity(typeof(FindAFamFormActivity));
            };

            create_fam_button.Click += delegate {
                StartActivity(typeof(CreateAFamFormActivity));
            };

            find_event_button.Click += delegate {   
                StartActivity(typeof(FindAnEventActivity));
            };

            create_event_button.Click += delegate {
                StartActivity(typeof(CreateEventActivity));
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
