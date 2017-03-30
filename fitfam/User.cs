using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace fitfam
{
    class User
    {
        private string userId;
        private string username;
        private List<string> activities;
        private Dictionary<string, bool> availability;
        private string bio;
        private string experienceLevel; //change to the type where it's 1 of a couple options
        private List<Group> fitFams;
        private List<User> friends;
        private string gender;
        private string pic;
        private List<Event> sharedEvents;

        public User()
        {

        }
    }
}