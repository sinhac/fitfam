using Amazon.DynamoDBv2.Model;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Globalization;

/*
 * FitFam 
 * 
 * Ehsan Ahmed, Jessa Marie Barre, Shannon Fisher, 
 * Josh Jacobson, Korey Prendergast, Chandrika Sinha
 * 4/20/2017
 * 
 * CreatAFamFormActivity: The page that allows you to create a group aka "fam"
 * User can add preferences to a new group to add to the database
 * 
 */

namespace fitfam
{
    [Activity(Label = "CreateafamformActivity")]
    public class CreateAFamFormActivity : Activity
    {
        // private member variables
        private string userId;
        private User creator;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CreateAFamForm);
            
            // get information from previous page
            userId = Intent.GetStringExtra("userId") ?? "null";
            creator = new User(userId, true);
            var pic = Intent.GetStringExtra("pic") ?? "null";
            var location = Intent.GetStringExtra("location") ?? "null";
            var username = Intent.GetStringExtra("username") ?? "null";
            var genderInt = Intent.GetIntExtra("gender", -1);
            var profileId = Intent.GetStringExtra("profileId") ?? "Null";

            // navbar buttons
            ImageButton homepageButton = FindViewById<ImageButton>(Resource.Id.homepageButton);
            homepageButton.Click += delegate
            {
                Intent intent = new Intent(this, typeof(HomepageActivity));
                intent.PutExtra("userId", userId);
                intent.PutExtra("profileId", userId);
                intent.PutExtra("pic", pic);
                intent.PutExtra("location", location);
                intent.PutExtra("username", username);
                intent.PutExtra("gender", genderInt);
                StartActivity(intent);
            };

            ImageButton profileButton = FindViewById<ImageButton>(Resource.Id.profileButton);
            profileButton.Click += delegate
            {
                Intent intent = new Intent(this, typeof(ProfilePageActivity));
                intent.PutExtra("userId", userId);
                intent.PutExtra("profileId", userId);
                intent.PutExtra("username", username);
                intent.PutExtra("pic", pic);
                intent.PutExtra("location", location);
                intent.PutExtra("gender", genderInt);
                StartActivity(intent);
            };

            ImageButton notificationsButton = FindViewById<ImageButton>(Resource.Id.notificationsButton);
            notificationsButton.Click += delegate
            {
                Intent intent = new Intent(this, typeof(NotificationsActivity));
                intent.PutExtra("userId", userId);
                intent.PutExtra("profileId", userId);
                intent.PutExtra("pic", pic);
                intent.PutExtra("location", location);
                intent.PutExtra("username", username);
                intent.PutExtra("gender", genderInt);
                StartActivity(intent);

            };

            ImageButton scheduleButton = FindViewById<ImageButton>(Resource.Id.scheduleButton);
            scheduleButton.Click += delegate
            {
                Intent intent = new Intent(this, typeof(ScheduleActivity));
                intent.PutExtra("userId", userId);
                intent.PutExtra("profileId", userId);
                intent.PutExtra("pic", pic);
                intent.PutExtra("location", location);
                intent.PutExtra("username", username);
                intent.PutExtra("gender", genderInt);
                StartActivity(intent);
            };

            // capture user input for fam name
            var famName = FindViewById<EditText>(Resource.Id.famNameEditText);
            var famNameInput = "";
            famName.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                famNameInput = e.Text.ToString();
            };

            // capture user input for description
            var description = FindViewById<EditText>(Resource.Id.descriptionEditText);
            var descriptionInput = "";
            description.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                descriptionInput = e.Text.ToString();
            };

            // capture user input for activity tags user is interested in
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
            
            // capture user input on boost value
            // boost value number will later be used for monetary reasons
            EditText boostText = FindViewById<EditText>(Resource.Id.boost);
            var boostInput = "";
            boostText.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                boostInput = e.Text.ToString();
            };

            // add user input to new entry in database, then redirects
            Button createFamButton = FindViewById<Button>(Resource.Id.createFamButton);
            createFamButton.Click += delegate {
                TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
                char[] delimiters = { ',', '\t', '\n' };
                string[] tagsArr = tagsInput.Split(delimiters);
                for (int i = 0; i < tagsArr.Length; i++)
                {
                    if (tagsArr[i] != " ")
                    {
                        tagsList.Add(myTI.ToLower(tagsArr[i]));
                        Console.WriteLine(tagsArr[i]);
                    }
                }
                
                var members = new Dictionary<User, bool>();
                members.Add(creator, true);
                if(boostInput == "") { boostInput = "0"; }
                double boost = double.Parse(boostInput, System.Globalization.CultureInfo.InvariantCulture);
                Group fam = new Group(famNameInput, descriptionInput, creator, members, tagsList, boost);
                /*foreach (var tag in tagsList)
                {
                    fam.addTag(tag);
                }*/
                creator.addFitFam(fam);
                var intent = new Intent(this, typeof(FamDetailsPageActivity));
                intent.PutExtra("groupId", fam.GroupId);
                intent.PutExtra("userId", userId);
                intent.PutExtra("profileId", userId);
                intent.PutExtra("pic", pic);
                intent.PutExtra("location", location);
                intent.PutExtra("username", username);
                intent.PutExtra("gender", genderInt);
                StartActivity(intent);
            };
        }
    }
}