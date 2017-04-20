using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace fitfam
{
    [Activity(Label = "FindafamformActivity")]
    public class FindAFamFormActivity : Activity
    {
        private object spinner_ItemSelected;
        private string userId;
        private User user;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            userId = Intent.GetStringExtra("userId") ?? "null";
            user = new User(userId, true);
            SetContentView(Resource.Layout.FindAFamForm);

            // Create your application here
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner1);

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.experience_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
            


            var tags = FindViewById<MultiAutoCompleteTextView>(Resource.Id.multiAutoCompleteTextView1);
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
            char[] delimiters = { ',', '\t', '\n' };
            tagsArr = tagsInput.Split(delimiters);
            for (int i = 0; i < tagsArr.Length; i++)
            {
                tagsList.Add(tagsArr[i]);
            }

            var cityZip = FindViewById<EditText>(Resource.Id.cityzip);
            var cityZipInput = "";
            cityZip.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                cityZipInput = e.Text.ToString();
            };

            Button button2 = FindViewById<Button>(Resource.Id.button2);
            button2.Click += delegate {
                var experienceLevel = (string)spinner.GetItemAtPosition(spinner.SelectedItemPosition);
                FindAFam famSearch = new FindAFam(user, tagsList, experienceLevel);
                Intent intent = new Intent(this, typeof(MatchesActivity));
                var results = famSearch.FamSearchResults;            
                intent.PutExtra("matches", results.ToArray());
                StartActivity(intent);
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
        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            string toast = string.Format("My experience level is {0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }
    }
}