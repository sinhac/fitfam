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
        public string UserId
        {
            get { return userId; }
        }
        private string username;
        public string Username
        {
            get { return username; }
        }
        private List<string> activities;
        public List<string> Activities
        {
            get { return activities; }
        }
        private Dictionary<string, bool> availability;
        public Dictionary<string, bool> Availability
        {
            get { return availability; }
        }
        private bool availabilitySet;
        public bool AvailabilitySet
        {
            get { return availabilitySet; }
        }
        private string bio;
        public string Bio
        {
            get { return bio; }
            set { bio = value; }
        }
        private string experienceLevel; //change to enum type
        public string ExperienceLevel
        {
            get { return experienceLevel; }
            set { experienceLevel = value; }
        }
        private List<Group> fitFams;
        public List<Group> FitFams
        {
            get { return fitFams; }
        }
        private List<User> friends;
        public List<User> Friends
        {
            get { return friends; }
        }
        private string gender;
        public string Gender
        {
            get { return gender; }
        }
        private string pic;
        public string Pic
        {
            get { return pic; }
            set { pic = value; }
        }
        private List<Event> sharedEvents;
        public List<Event> SharedEvents
        {
            get { return sharedEvents; }
        }

        public User(string userId)
        {
            //get user data
        }

    }
}