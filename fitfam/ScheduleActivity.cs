using Android.App;
using Android.OS;
using Android.Widget;

namespace fitfam
{
    [Activity(Label = "ScheduleActivity")]
    public class ScheduleActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Schedule);

            // Create your application here
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

            /*

            CalendarView calendarView;
            TextView dateDisplay;

            calendarView = (CalendarView)FindViewById(Resource.Id.calendarView1);
            dateDisplay = (TextView)FindViewById(Resource.Id.date_display);
            dateDisplay.SetText(Resource.String.Date);

            /*
            calendarView.SetOnDateChangeListener(new CalendarView.IOnDateChangeListener() {
            @Override
            void onSelectedDayChange(CalendarView cv, int i, int i1, int i2)
            {
                dateDisplay.SetText(Resource.String.Datee);

                Toast.MakeText(, "Selected Date:\n" + "Day = " + i2 + "\n" + "Month = " + i1 + "\n" + "Year = " + i, ToastLength.Long).show();
            }
            });*/
        }
    }
}