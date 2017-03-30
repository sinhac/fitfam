using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace fitfam
{
    [Activity(Label = "CreateafamformActivity")]
    public class CreateafamformActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Createafamform);

            // Create your application here
            var famName = FindViewById<EditText>(Resource.Id.famName);
            var famNameInput = "";
            famName.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                famNameInput = e.Text.ToString();
            };

            var description = FindViewById<EditText>(Resource.Id.description);
            var descriptionInput = "";
            description.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                descriptionInput = e.Text.ToString();
            };
            
            Button button2 = FindViewById<Button>(Resource.Id.button2);
            button2.Click += delegate {
                // var newActivity = new Intent(this, typeof(FamProfileActivity));
                // newActivity.PutExtra("Fam Name", famNameInput);
                // StartActivity(newActivity);
                StartActivity(typeof(matchesActivity));
            };

            Finish();
        }
    }
}