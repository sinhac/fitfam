using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace fitfam
{
    [Activity(Label = "CreateafamformActivity")]
    public class CreateAFamFormActivity : Activity
    {
        private string userId;
        private User creator;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            System.Console.WriteLine("opened createfam form");
            base.OnCreate(savedInstanceState);
            userId = Intent.GetStringExtra("userId") ?? "Null";
            creator = new User(userId, true);
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
            List<string> tagsList = new List<string>();
            tags.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                tagsInput = e.Text.ToString();
            };
            

            EditText boostText = FindViewById<EditText>(Resource.Id.boost);
            var boostInput = "";
            boostText.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                boostInput = e.Text.ToString();
            };

            /* add user input to new entry in database, then redirect */
            Button createFamButton = FindViewById<Button>(Resource.Id.createFamButton);
            createFamButton.Click += delegate {
                TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
                char[] delimiters = { ',', '\t', '\n' };
                string[] tagsArr = tagsInput.Split(delimiters);
                for (int i = 0; i < tagsArr.Length; i++)
                {
                    tagsList.Add(myTI.ToLower(tagsArr[i]));
                }
               
                System.Console.WriteLine("creating fam");
                
                var members = new Dictionary<User, bool>();
                members.Add(creator, true);
                if(boostInput == "") { boostInput = "0"; }
                double boost = double.Parse(boostInput, System.Globalization.CultureInfo.InvariantCulture);
                Group fam = new Group(famNameInput, descriptionInput, creator, members, tagsList, boost);
                foreach (var tag in tagsList)
                {
                    fam.addTag(tag);
                }

                System.Console.WriteLine("Created fam");
                var famDetailsActivity = new Intent(this, typeof(FamDetailsPageActivity));
                famDetailsActivity.PutExtra("groupId", fam.GroupId);
                famDetailsActivity.PutExtra("userID", userId);
                StartActivity(famDetailsActivity);
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