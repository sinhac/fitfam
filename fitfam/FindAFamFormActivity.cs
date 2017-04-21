using Amazon.DynamoDBv2.Model;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;

/*
 * FitFam 
 * 
 * Ehsan Ahmed, Jessa Marie Barre, Shannon Fisher, 
 * Josh Jacobson, Korey Prendergast, Chandrika Sinha
 * 4/20/2017
 * 
 * FindAFamFormActivity: The page to search for groups aka "fams"
 * Search for fams using filters such as activity
 * 
 */

namespace fitfam
{
    [Activity(Label = "FindafamformActivity")]
    public class FindAFamFormActivity : Activity
    {
        // private member variables
        private object spinner_ItemSelected;
        private string userId;
        private User user;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FindAFamForm);

            // get information from previous page
            userId = Intent.GetStringExtra("userId") ?? "null";
            var pic = Intent.GetStringExtra("pic") ?? "null";
            var location = Intent.GetStringExtra("location") ?? "null";
            var username = Intent.GetStringExtra("username") ?? "null";
            var genderInt = Intent.GetIntExtra("gender", -1);
            var profileId = Intent.GetStringExtra("profileId") ?? "Null";
            user = new User(userId, true);

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

            // set the preferred activities
            var tags = FindViewById<MultiAutoCompleteTextView>(Resource.Id.activityTags);
            var adapter2 = ArrayAdapter.CreateFromResource(this, Resource.Array.ACTIVITIES, Android.Resource.Layout.SimpleSpinnerItem);
            adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            tags.Adapter = adapter2;
            tags.SetTokenizer(new MultiAutoCompleteTextView.CommaTokenizer());

            var tagsInput = "";
            string[] tagsArr;
            List<string> tagsList = new List<string>();
            tags.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                tagsInput = e.Text.ToString();
            };

            // choose the experience level
            Spinner experienceSpinner = FindViewById<Spinner>(Resource.Id.experienceSpinner);

            experienceSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.experience_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            experienceSpinner.Adapter = adapter;
            
            // button that routes to the matches page when ready to find groups
            Button findFamButton = FindViewById<Button>(Resource.Id.findFamButton);
            findFamButton.Click += async delegate {
                System.Console.WriteLine("Entered button click");
                var experienceLevel = (string)experienceSpinner.GetItemAtPosition(experienceSpinner.SelectedItemPosition);
                var results = new List<string>();
                var awsClient = new AWSClient(Amazon.RegionEndpoint.USEast1);
                var client = awsClient.getDynamoDBClient();
                System.Console.WriteLine("Finished basic business");

                char[] delimiters = { ',', '\t', '\n' };
                tagsArr = tagsInput.Split(delimiters);
                for (int i = 0; i < tagsArr.Length; i++)
                {
                    tagsList.Add(tagsArr[i]);
                }
                System.Console.WriteLine("Finished tags array, size " + tagsList.Count);

                var request = new ScanRequest
                {
                    TableName = "fitfam-mobilehub-2083376203-groups",
                };
                var response = await client.ScanAsync(request);
                var result = response.Items;
                System.Console.WriteLine("Looking for results");

                // algorithm to find matches
                int numRows = 0;
                foreach (Dictionary<string, AttributeValue> item in result)
                {
                    numRows++;
                    int numMatches = 0;
                    string groupId = item["groupId"].S;

                    foreach (var kvp in item)
                    {
                        if (kvp.Key == "tags")
                        {
                            var groupTags = kvp.Value.L;
                            foreach (var t in groupTags)
                            {
                                string s = t.S;
                                foreach (string tag in tagsList)
                                {
                                    Console.WriteLine(s + " compared to " + tag);
                                    if (s.ToLower() == tag.ToLower())
                                    {
                                        numMatches++;
                                    }
                                }
                            }
                        }
                        if (kvp.Key == "experienceLevel")
                        {
                            var experienceLevels = kvp.Value.L;
                            int numExp = 0;
                            foreach (var e in experienceLevels)
                            {
                                string s = e.S;
                                numExp++;
                                if (s[0] == experienceLevel[0])
                                {
                                    numMatches++;
                                }
                            }
                        }
                    }
                    if(numMatches >= 2)
                    {
                        System.Console.WriteLine("Number of matches found: " + numMatches);
                        results.Add(item["groupId"].S);
                        System.Console.WriteLine("added to group? I think? Size of results: " + results.Count);
                    }
                }
                System.Console.WriteLine("Made it past the for loop.");

                Intent intent = new Intent(this, typeof(MatchesActivity));
                System.Console.WriteLine("Starting intent stuff");
                intent.PutExtra("matches", results.ToArray());
                intent.PutExtra("userId", userId);
                intent.PutExtra("profileId", userId);
                intent.PutExtra("pic", pic);
                intent.PutExtra("location", location);
                intent.PutExtra("username", username);
                intent.PutExtra("gender", user.Gender);
                System.Console.WriteLine("Ended intent stuff, heading to activity");
                StartActivity(intent);
            };
        }

        // function that helps spinner work
        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
        }
    }
}