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
using Amazon.DynamoDBv2.Model;

namespace fitfam
{
    class User
    {
        private string userId;
        public string UserId
        {
            get { return userId; }
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

        /// <summary>
        /// Creates user object
        /// </summary>
        /// <param name="userId"></param>
        public User(string userId)
        {
            this.userId = userId;                        
        }
        
        /// <summary>
        /// Enters user entry to table
        /// Should only be used once per user
        /// </summary>
        public void createEntry()
        {
            using (var awsClient = new AWSClient(Amazon.RegionEndpoint.USEast1))
            {
                using (var client = awsClient.getDynamoDBClient())
                {
                    var table = "fitfam-mobilehub-2083376203-users";
                    var item = new Dictionary<string, AttributeValue>()
                    {
                        {"userId", new AttributeValue { S = userId } }
                    };
                    awsClient.putItem(client, awsClient.makePutRequest(table, item));                   
                }
            }      
        }

        public void getEntry()
        {
            using (var awsClient = new AWSClient(Amazon.RegionEndpoint.USEast1))
            {
                using (var client = awsClient.getDynamoDBClient())
                {
                    var table = "fitfam-mobilehub-2083376203-users";
                    var key = new Dictionary<string, AttributeValue>()
                    {
                        {"userId", new AttributeValue { S = userId } }
                    };
                    var response = awsClient.getItem(client, awsClient.makeGetRequest(table, key));
                    var userMap = response.Item;
                    if (userMap.ContainsKey("activities"))
                        activities = userMap["activities"].SS;
                   // if (userMap.ContainsKey("availability"))
                     //   availability = userMap["availability"].M;
                }
            }
        }

    }
}