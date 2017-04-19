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
using Amazon.DynamoDBv2.DocumentModel;

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
            set {
                pic = value;
                // update DB
                // create request to set pic in database
                AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
                Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
                var request = new UpdateItemRequest
                {
                    TableName = "fitfam-mobilehub-2083376203-users",
                    Key = new Dictionary<string, AttributeValue>() { { "userId", new AttributeValue { S = username } } },
                    ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#P", "pics"},  // attribute to be updated
                },
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":newPic",new AttributeValue { S = pic }},  // new pic to update user's pic with 
                },

                    // expression to set pic in database entry
                    UpdateExpression = "SET #P :newPic"
                };
                var response = dbclient.UpdateItemAsync(request);
            }
        }
        private List<Event> sharedEvents;
        public List<Event> SharedEvents
        {
            get { return sharedEvents; }
        }

        public User(string userId)
        {
            this.userId = userId;
            this.setValues();
        }
        
        private async void setValues()
        {
            using (var awsClient = new AWSClient(Amazon.RegionEndpoint.USEast1))
            {
                using (var client = awsClient.getDynamoDBClient())
                {
                    var key = new Dictionary<string, AttributeValue>() { { "userId", new AttributeValue { S = userId } } };
                    var request = awsClient.makeGetRequest("fitfam-mobilehub-2083376203-users", key);
                    Dictionary<string, AttributeValue> userInfo = new Dictionary<string, AttributeValue>();
                    var task = await awsClient.GetItemAsync(client, request);
                    userInfo = task;
                    if (task.Values.Count == 0)
                    {
                        createEntry();
                    }
                    else
                    {

                    }
                }
            }
        }

        public void createEntry()
        {
            using (var awsClient = new AWSClient(Amazon.RegionEndpoint.USEast1))
            {
                using (var client = awsClient.getDynamoDBClient())
                {
                    Dictionary<string, AttributeValue> item = new Dictionary<string, AttributeValue>();
                    var abc = awsClient.makePutRequest("fitfam-mobilehub-2083376203-users", item);
                    System.Console.WriteLine(abc);
                }
            }      
        }

        public void addFam(string groupId)
        {
            //var newGroup = new Group(groupId);

        }
    }
}