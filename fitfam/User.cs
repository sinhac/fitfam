using System;
using System.Collections.Generic;
using System.Linq;
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
        private List<string> activities = new List<string>();
        public List<string> Activities
        {
            get { return activities; }
        }
        public void addActivity(string activity)
        {
            String expression;
            if (activities.Count == 0)
            {
                expression = "SET #A = :newActivity";
                activities.Add(activity);
                AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
                Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
                var request = new UpdateItemRequest
                {
                    TableName = "fitfam-mobilehub-2083376203-users",
                    Key = new Dictionary<string, AttributeValue>() { { "userId", new AttributeValue { S = userId } } },
                    ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#A", "activities"},  // attribute to be updated
                },
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":newActivity",new AttributeValue { SS = new List<string>{ activity } } },  // new activity to update user's activities with 
                },
                    // activity added to list in database entry
                    UpdateExpression = expression
                };
                var response = dbclient.UpdateItemAsync(request);
            }
            else
            {
                expression = "ADD #A  :newActivity";
                activities.Add(activity);
                AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
                Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
                var request = new UpdateItemRequest
                {
                    TableName = "fitfam-mobilehub-2083376203-users",
                    Key = new Dictionary<string, AttributeValue>() { { "userId", new AttributeValue { S = userId } } },
                    ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#A", "activities"},  // attribute to be updated
                },
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":newActivity",new AttributeValue { S = activity }},  // new activity to update user's activities with 
                },
                    // activity added to list in database entry
                    UpdateExpression = expression
                };
                var response = dbclient.UpdateItemAsync(request);
            }
           
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
                Console.WriteLine("setting bio");
                bio = value;
                using (AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1))
                {
                    using (var dbclient = awsclient.getDynamoDBClient())
                    {
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
                                {":newBio",new AttributeValue { S = bio }},  // new bio to update user's bio with 
                            },

                            // expression to set pic in database entry
                            UpdateExpression = "SET #B = :newBio"
                        };
                        var response = dbclient.UpdateItemAsync(request);
                    }
                        
                }
                
            }
        }
        private string experienceLevel; //change to enum type
        public string ExperienceLevel
        {
            get { return experienceLevel; }
            set
            {
                experienceLevel = value;
                AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
                Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
                var request = new UpdateItemRequest
                {
                    TableName = "fitfam-mobilehub-2083376203-users",
                    Key = new Dictionary<string, AttributeValue>() { { "userId", new AttributeValue { S = userId } } },
                    ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#E", "experienceLevel"},  // attribute to be updated
                },
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":newExperienceLevel",new AttributeValue { S = experienceLevel }},  
                },
                    UpdateExpression = "SET #E = :newExperienceLevel"
                };
                var response = dbclient.UpdateItemAsync(request);
                
            }
        }
        private List<Group> fitFams = new List<Group>();
        public List<Group> FitFams
        {
            get { return fitFams; }
        }
        public void addFitFam(Group group)
        {
            string expression;
            if (fitFams.Count == 0)
            {
                expression = "SET #G = :newFitFam";
                fitFams.Add(group);
                AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
                Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
                var request = new UpdateItemRequest
                {
                    TableName = "fitfam-mobilehub-2083376203-users",
                    Key = new Dictionary<string, AttributeValue>() { { "userId", new AttributeValue { S = userId } } },
                    ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#G", "fitFams"},  // attribute to be updated
                },
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":newFitFam",new AttributeValue { SS = new List<string>{ group.GroupId } }},  
                },

                    // fitFam added to list in database entry
                    UpdateExpression = expression
                };
                var response = dbclient.UpdateItemAsync(request);
            }
            else
            {
                expression = "ADD #G :newFitFam";
                fitFams.Add(group);
                AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
                Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
                var request = new UpdateItemRequest
                {
                    TableName = "fitfam-mobilehub-2083376203-users",
                    Key = new Dictionary<string, AttributeValue>() { { "userId", new AttributeValue { S = userId } } },
                    ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#G", "fitFams"},  // attribute to be updated
                },
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":newFitFam",new AttributeValue { S = group.GroupId }}, 
                },
                    UpdateExpression = expression
                };
                var response = dbclient.UpdateItemAsync(request);
            }
            
            
        }
        public void removeFitFam(Group group)
        {
            fitFams.Remove(group);
            AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
            Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
            var request = new UpdateItemRequest
            {
                TableName = "fitfam-mobilehub-2083376203-users",
                Key = new Dictionary<string, AttributeValue>() { { "userId", new AttributeValue { S = userId } } },
                ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#G", "fitFams"},  // attribute to be updated
                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":oldFitFam",new AttributeValue { S = group.GroupId }},  // new activity to update user's activities with 
                },

                // activity added to list in database entry
                UpdateExpression = "DELETE #G :oldFitFam"
            };
            var response = dbclient.UpdateItemAsync(request);
        }
        private Group userFam;
        public Group UserFam
        {
            get { return userFam; }
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
        public void addJoinedEvent(Event joinedEvent)
        {
            string expression;
            if (joinedEvents.Count == 0)
            {
                expression = "SET #JE = :newJoinedEvent";
                joinedEvents.Add(joinedEvent);
                AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
                Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
                var request = new UpdateItemRequest
                {
                    TableName = "fitfam-mobilehub-2083376203-users",
                    Key = new Dictionary<string, AttributeValue>() { { "userId", new AttributeValue { S = userId } } },
                    ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#JE", "joinedEvents"},  // attribute to be updated
                },
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":newJoinedEvent",new AttributeValue { SS = new List<string>{ joinedEvent.EventId } }},  
                },

                    UpdateExpression = expression
                };
                var response = dbclient.UpdateItemAsync(request);
            }
            else
            {
                expression = "ADD #JE :newJoinedEvent";
                joinedEvents.Add(joinedEvent);
                AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
                Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
                var request = new UpdateItemRequest
                {
                    TableName = "fitfam-mobilehub-2083376203-users",
                    Key = new Dictionary<string, AttributeValue>() { { "userId", new AttributeValue { S = userId } } },
                    ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#JE", "joinedEvents"},  // attribute to be updated
                },
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":newJoinedEvent",new AttributeValue { S = joinedEvent.EventId }},  
                },
                    UpdateExpression = expression
                };
                var response = dbclient.UpdateItemAsync(request);
            }
            
        }
        public void removeJoinedEvent(Event joinedEvent)
        {
            joinedEvents.Remove(joinedEvent);
            AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
            Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
            var request = new UpdateItemRequest
            {
                TableName = "fitfam-mobilehub-2083376203-users",
                Key = new Dictionary<string, AttributeValue>() { { "userId", new AttributeValue { S = userId } } },
                ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#JE", "joinedEvents"},  // attribute to be updated
                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":oldJoinedEvent",new AttributeValue { S = joinedEvent.EventId }},  // new activity to update user's activities with 
                },

                // activity added to list in database entry
                UpdateExpression = "DELETE #JE :oldJoinedEvent"
            };
            var response = dbclient.UpdateItemAsync(request);
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
                    UpdateExpression = "SET #P = :newPic"
                };
                var response = dbclient.UpdateItemAsync(request);
            }
        }
        private List<Event> sharedEvents = new List<Event>();
        public List<Event> SharedEvents
        {
            get { return sharedEvents; }
        }
        public void addSharedEvent(Event sharedEvent)
        {
            string expression;
            if (sharedEvents.Count == 0)
            {
                expression = "SET #SE = :newSharedEvent";
            }
            else
            {
                expression = "ADD #SE :newSharedEvent";
            }
            sharedEvents.Add(sharedEvent);
            AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
            Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
            var request = new UpdateItemRequest
            {
                TableName = "fitfam-mobilehub-2083376203-users",
                Key = new Dictionary<string, AttributeValue>() { { "userId", new AttributeValue { S = userId } } },
                ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#SE", "sharedEvent"},  // attribute to be updated
                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":newSharedEvent",new AttributeValue { S = sharedEvent.EventId }},  
                },

                // event added to list in database entry
                UpdateExpression = expression
            };
            var response = dbclient.UpdateItemAsync(request);
        }
        public void removeSharedEvent(Event sharedEvent)
        {
          
            sharedEvents.Remove(sharedEvent);
            AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
            Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
            var request = new UpdateItemRequest
            {
                TableName = "fitfam-mobilehub-2083376203-users",
                Key = new Dictionary<string, AttributeValue>() { { "userId", new AttributeValue { S = userId } } },
                ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#SE", "sharedEvent"},  // attribute to be updated
                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":oldSharedEvent",new AttributeValue { S = sharedEvent.EventId }}, 
                },

                // shared event added to list in database entry
                UpdateExpression = "DELETE #SE oldSharedEvent"
            };
            var response = dbclient.UpdateItemAsync(request);
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
                    {":newLocation",new AttributeValue { S = location }},  
                },

                    // expression to set location in database entry
                    UpdateExpression = "SET #L = :newLocation"
                };
                var response = dbclient.UpdateItemAsync(request);
            }
        }

        private async void UpdateItemAsync(Amazon.DynamoDBv2.AmazonDynamoDBClient client, UpdateItemRequest request)
        {
            try
            {
                var response = await client.UpdateItemAsync(request);
                Console.WriteLine("bio updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine("UPDATE EXCEPTION: {0}, {1}", ex.Message, ex.ToString());
            }
            
            //return response;
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
            Console.WriteLine("making user");
            this.userId = userId;
            var userFamId = userId + "My Fam";
            userFam = new Group(userFamId);
            this.setValues(mainUser);
        }
        
        private async void setValues(bool mainUser)
        {
            Console.WriteLine("setting values");
            using (var awsClient = new AWSClient(Amazon.RegionEndpoint.USEast1))
            {
                using (var client = awsClient.getDynamoDBClient())
                {
                    var expression = new Dictionary<string, AttributeValue> { { ":v_userId", new AttributeValue { S = userId } } };
                    var requestQuery = awsClient.makeQueryRequest("fitfam-mobilehub-2083376203-users", "userId = :v_userId", expression);
                    var response = await client.QueryAsync(requestQuery);
                    
                    Console.WriteLine("got response");
                    if (response.Count == 0)
                    {
                        createEntry();
                    }
                    else
                    {
                        var key = new Dictionary<string, AttributeValue>() { { "userId", new AttributeValue { S = userId } } };
                        var request = awsClient.makeGetRequest("fitfam-mobilehub-2083376203-users", key);
                        Dictionary<string, AttributeValue> userInfo = new Dictionary<string, AttributeValue>();
                        var task = await awsClient.GetItemAsync(client, request);
                        userInfo = task;
                       
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
                                        var members = new Dictionary<User, bool>();
                                        members.Add(this, true);
                                       
                                        var friendsList = kvp.Value.SS.ToList<string>();
                                        foreach (var friendId in friendsList)
                                        {                                        
                                            userFam.addMember(new User(friendId, false), false);   
                                        }
                                    }
                                    else
                                    {
                                        userFam = null;
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
                                    break;

                            }
                        }
                    }
                }
            }
        }

        public async void createEntry()
        {
            Console.WriteLine("making entry");
            using (var awsClient = new AWSClient(Amazon.RegionEndpoint.USEast1))
            {
                using (var client = awsClient.getDynamoDBClient())
                {
                    Console.WriteLine("writing to database");
                   
                    try
                    {
                        var table = awsClient.getTable(client, "fitfam-mobilehub-2083376203-users");
                        var userDoc = new Document();
                        userDoc["userId"] = this.userId;
                        userDoc["activities"] = new List<string>();
                        userDoc["fitFams"] = new List<string>();
                        userDoc["friends"] = new List<string>();
                        userDoc["gender"] = this.gender;
                        userDoc["joinedEvents"] = new List<string>();
                        userDoc["pic"] = pic;
                        userDoc["sharedEvents"] = new List<string>();
                        userDoc["location"] = this.location;
                        userDoc["username"] = this.username;
                        userDoc["bio"] = " ";
                        var response = await table.PutItemAsync(userDoc);
                        Console.WriteLine("made entry {0}", response);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("EXXXCEPTION: {0}, {1}", ex.ToString(), ex.Message);
                    }
                }
            }
            var members = new Dictionary<User, bool>();
            members.Add(this, true);
            userFam = new Group(String.Format("{0}'s Fam", this.username), "These are other FitFam users you have connected with", this, members, activities, 0);
            userFam.addExperienceLevel(experienceLevel);
            addFitFam(userFam);
        }

        public void acceptJoinRequest(User user, Group group)
        {
            group.acceptJoinRequest(user);
            if (group.GroupName == this.username + "'s Fam")
            {
                //add user to the other users my fam group
                var groupId = user.userId + user.username + "'s Fam";
                var userGroup = new Group(groupId);
                userGroup.addMember(user, false);
                this.addFitFam(userGroup);
            }
        }

        public void attendingEvent(Event evnt)
        {
            addJoinedEvent(evnt);
            evnt.addAttending(this);
        }

        public void notAttendingEvent(Event evnt)
        {
            removeJoinedEvent(evnt);
            evnt.removeAttending(this);
        }
    }
}