using Android.App;
using Android.OS;
using Android.Widget;
using System.Collections.Generic;

namespace fitfam
{
    [Activity(Label = "CreateafamformActivity")]
    public class CreateAFamFormActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            System.Console.WriteLine("opened createfam form");
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CreateAFamForm);

            // capture user input for Fam name, description, and tags
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

            var tags = FindViewById<MultiAutoCompleteTextView>(Resource.Id.actTag);
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

            // add user input to new entry in database, then redirect
            Button button2 = FindViewById<Button>(Resource.Id.button2);
            button2.Click += delegate {
                System.Console.WriteLine("creating fam");
                Group fam = new Group(famNameInput, descriptionInput, new User("test"));
                System.Console.WriteLine("Created fam");
                StartActivity(typeof(FamProfileActivity));
            };

            /* navbar buttons */
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