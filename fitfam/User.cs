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
        private Dictionary<string, bool> availability = new Dictionary<string, bool>();
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
            set
            {
                bio = value;
                AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
                Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
                var request = new UpdateItemRequest
                {
                    TableName = "fitfam-mobilehub-2083376203-users",
                    Key = new Dictionary<string, AttributeValue>() { { "userId", new AttributeValue { S = userId } } },
                    ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#B", "bio"},  // attribute to be updated
                },
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":newBio",new AttributeValue { S = bio }},  // new pic to update user's pic with 
                },

                    // expression to set pic in database entry
                    UpdateExpression = "SET #B :newBio"
                };
                var response = dbclient.UpdateItemAsync(request);
            }
        }
        private string experienceLevel; //change to enum type
        public string ExperienceLevel
        {
            get { return experienceLevel; }
            set { experienceLevel = value; }
        }
        private List<Group> fitFams = new List<Group>();
        public List<Group> FitFams
        {
            get { return fitFams; }
        }
        private List<User> friends = new List<User>();
        public List<User> Friends
        {
            get { return friends; }
        }
        private string gender;
        public string Gender
        {
            get { return gender; }
        }
        private List<Event> joinedEvents = new List<Event>();
        public List<Event> JoinedEvents
        {
            get { return joinedEvents; }
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
                    Key = new Dictionary<string, AttributeValue>() { { "userId", new AttributeValue { S = userId } } },
                    ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#P", "pic"},  // attribute to be updated
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
        private List<Event> sharedEvents = new List<Event>();
        public List<Event> SharedEvents
        {
            get { return sharedEvents; }
        }
        private string location;
        public string Location
        {
            get { return location; }
            set
            {
                location = value;
                AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
                Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
                var request = new UpdateItemRequest
                {
                    TableName = "fitfam-mobilehub-2083376203-users",
                    Key = new Dictionary<string, AttributeValue>() { { "userId", new AttributeValue { S = userId } } },
                    ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#L", "location"},  // attribute to be updated
                },
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":newLocation",new AttributeValue { S = location }},  // new pic to update user's pic with 
                },

                    // expression to set pic in database entry
                    UpdateExpression = "SET #L :newLocation"
                };
                var response = dbclient.UpdateItemAsync(request);
            }
        }


        public User(string userId, string gender, string pic, string location, string username)
        {
            this.userId = userId;
            this.gender = gender;
            this.pic = pic;
            this.location = location;
            this.username = username;
            setValues(true);
        }

        public User(string userId, bool mainUser)
        {
            this.userId = userId;
            this.setValues(mainUser);
        }
        
        private async void setValues(bool mainUser)
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
                        foreach (KeyValuePair<string, AttributeValue> kvp in userInfo)
                        {
                            switch (kvp.Key)
                            {
                                case "activities":
                                    activities = kvp.Value.SS.ToList<string>();
                                    break;
                                case "availability":
                                    this.availabilitySet = true;
                                    var availabilityMap = kvp.Value.M;
                                    foreach (var keyval in availabilityMap)
                                    {
                                        availability.Add(keyval.Key, keyval.Value.BOOL);
                                    }
                                    break;
                                case "bio":
                                    bio = kvp.Value.S;
                                    break;
                                case "experienceLevel":
                                    experienceLevel = kvp.Value.S;
                                    break;
                                case "fitFams":
                                    var fitFamList = kvp.Value.SS.ToList<string>();
                                    foreach (var groupId in fitFamList)
                                    {
                                        fitFams.Add(new Group(groupId));
                                    }
                                    break;
                                case "friends":
                                    if (mainUser)
                                    {
                                        var friendsList = kvp.Value.SS.ToList<string>();
                                        foreach (var friendId in friendsList)
                                        {
                                            friends.Add(new User(friendId, false));
                                        }
                                    }
                                    else
                                    {
                                        friends = null;
                                    }
                                    break;
                                case "gender":
                                    gender = kvp.Value.S;
                                    break;
                                case "joinedEvents":
                                    var joinedEventList = kvp.Value.SS.ToList<string>();
                                    foreach (var eventId in joinedEventList)
                                    {
                                        joinedEvents.Add(new Event(eventId));
                                    }
                                    break;
                                case "pic":
                                    pic = kvp.Value.S;
                                    break;
                                case "sharedEvents":
                                    var sharedEventsList = kvp.Value.SS.ToList<string>();
                                    foreach (var eventId in sharedEventsList)
                                    {
                                        sharedEvents.Add(new Event(eventId));
                                    }
                                    break;               
                                default:
                                    Console.WriteLine("done fucked up");
                                    break;

                            }
                        }
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
                    var table = awsClient.getTable(client, "fitfam-mobilehub-2083376203-users");
                    var userDoc = new Document();
                    userDoc["userId"] = userId;
                    userDoc["activities"] = new List<string>();
                    userDoc["fitFams"] = new List<string>();
                    userDoc["friends"] = new List<string>();
                    userDoc["gender"] = gender;
                    userDoc["joinedEvents"] = new List<string>();
                    userDoc["pic"] = pic;
                    userDoc["sharedEvents"] = new List<string>();
                    userDoc["location"] = location;
                    userDoc["username"] = username;
                    table.PutItemAsync(userDoc);                 
                }
            }      
        }

        public void addFam(string groupId)
        {
            
            //var newGroup = new Group(groupId);

        }
    }
}