using Android.App;
using Android.OS;
using Android.Widget;
using System.Collections.Generic;

namespace fitfam
{
    [Activity(Label = "CreateafamformActivity")]
    public class CreateAFamFormActivity : Activity
    {
        private string userId;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            System.Console.WriteLine("opened createfam form");
            base.OnCreate(savedInstanceState);
            userId = Intent.GetStringExtra("userId") ?? "Null";
            SetContentView(Resource.Layout.CreateAFamForm);

            // capture user input for Fam name, description, and tags
            var famName = FindViewById<EditText>(Resource.Id.famNameEditText);
            var famNameInput = "";
            famName.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                famNameInput = e.Text.ToString();
            };

            var description = FindViewById<EditText>(Resource.Id.descriptionEditText);
            var descriptionInput = "";
            description.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                descriptionInput = e.Text.ToString();
            };

            var tags = FindViewById<MultiAutoCompleteTextView>(Resource.Id.addTagsTextView);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.ACTIVITIES, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            tags.Adapter = adapter;
            tags.SetTokenizer(new MultiAutoCompleteTextView.CommaTokenizer());

            var tagsInput = "";
            string[] tagsArr;
            List<string> tagsList = new List<string>();
            tags.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                tagsInput = e.Text.ToString();
            };
            char[] delimiters = { ',', '\t', '\n' };
            tagsArr = tagsInput.Split(delimiters);
            for (int i = 0; i < tagsArr.Length; i++)
            {
                tagsList.Add(tagsArr[i]);
            }

            /* add user input to new entry in database, then redirect */
            Button createFamButton = FindViewById<Button>(Resource.Id.createFamButton);
            createFamButton.Click += delegate {
                System.Console.WriteLine("creating fam");
                var creator = new User(userId);
                Group fam = new Group(famNameInput, descriptionInput, creator);
                System.Console.WriteLine("Created fam");
                StartActivity(typeof(FamProfileActivity));
            };

            /* navbar buttons */
            ImageButton navBarHomeButton = FindViewById<ImageButton>(Resource.Id.navBarHomeButton);
            navBarHomeButton.Click += delegate {
                StartActivity(typeof(HomepageActivity));
            };

            ImageButton navBarProfileButton = FindViewById<ImageButton>(Resource.Id.navBarProfileButton);
            navBarProfileButton.Click += delegate {
                StartActivity(typeof(ProfilePageActivity));
            };

            ImageButton navBarNotificationsButton = FindViewById<ImageButton>(Resource.Id.navBarNotificationsButton);
            navBarNotificationsButton.Click += delegate {
                StartActivity(typeof(NotificationsActivity));
            };

            ImageButton navBarScheduleButton = FindViewById<ImageButton>(Resource.Id.navBarScheduleButton);
            navBarScheduleButton.Click += delegate {
                StartActivity(typeof(ScheduleActivity));
            };
        }
    }
}