using Android.App;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace fitfam
{
    [Activity(Label = "EditProfilePage")]
    public class EditProfilePageActivity : Activity
    {
        private string userId;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            userId = Intent.GetStringExtra("userId") ?? "Null";
            SetContentView(Resource.Layout.EditProfilePage);

            var bioEdit = FindViewById<EditText>(Resource.Id.bioEdit);
            var bioEditInput = "";
            bioEdit.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                bioEditInput = e.Text.ToString();
            };
            User currentUser = new User(userId, true);

            var activities = FindViewById<MultiAutoCompleteTextView>(Resource.Id.activityEdit);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.ACTIVITIES, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            activities.Adapter = adapter;
            activities.SetTokenizer(new MultiAutoCompleteTextView.CommaTokenizer());
            var activitiesInput = "";
            string[] activitiesArr;
            List<string> activitiesList = new List<string>();
            activities.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                activitiesInput = e.Text.ToString();
            };
            char[] delimiters = { ',', '\t', '\n' };
            activitiesArr = activitiesInput.Split(delimiters);
            
            /*var activityInput = "";
            activities.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                activityInput = e.Text.ToString();
            };*/


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

            Spinner spinner1 = FindViewById<Spinner>(Resource.Id.spinner1);

            spinner1.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var adapter1 = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.availability_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter1.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner1.Adapter = adapter1;

            Spinner spinner2 = FindViewById<Spinner>(Resource.Id.spinner2);

            spinner2.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var adapter2 = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.availability_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner2.Adapter = adapter2;

            Spinner spinner3 = FindViewById<Spinner>(Resource.Id.spinner3);

            spinner3.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var adapter3 = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.availability_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter3.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner3.Adapter = adapter3;

            Spinner spinner4 = FindViewById<Spinner>(Resource.Id.spinner4);

            spinner4.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var adapter4 = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.availability_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter4.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner4.Adapter = adapter4;

            Spinner spinner5 = FindViewById<Spinner>(Resource.Id.spinner5);

            spinner5.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var adapter5 = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.availability_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter5.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner5.Adapter = adapter5;

            Spinner spinner6 = FindViewById<Spinner>(Resource.Id.spinner6);

            spinner6.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var adapter6 = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.availability_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter6.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner6.Adapter = adapter6;

            Spinner spinner7 = FindViewById<Spinner>(Resource.Id.spinner7);

            spinner7.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var adapter7 = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.availability_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter7.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner7.Adapter = adapter7;

            Button savechanges_button = FindViewById<Button>(Resource.Id.save_changes_button);
            savechanges_button.Click += delegate
            {
                currentUser.Bio = bioEditInput;

                foreach (string act in activitiesArr)
                {
                    currentUser.addActivity(act);
                }
                
                StartActivity(typeof(ProfilePageActivity));
            };
        }
        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            string toast = string.Format("The preferred availability is {0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }
    }
}