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
using System.Globalization;

namespace fitfam
{
    class Group
    {
        private string creatorId;
        public string CreatorId
        {
            get { return creatorId; }
        }
        private string groupId;
        public string GroupId
        {
            get { return groupId; }
        }
        private string groupName;
        public string GroupName
        {
            get { return groupName; }
            set
            {
                groupName = value;
                using (AWSClient awsClient = new AWSClient(Amazon.RegionEndpoint.USEast1))
                {
                    using (var client = awsClient.getDynamoDBClient())
                    {
                        var key = new Dictionary<string, AttributeValue>() { { "groupId", new AttributeValue { S = groupId } } };
                        var attrNames = new Dictionary<string, string>()
                        {
                            {"#GN", "groupName" }
                        };
                            var attrVals = new Dictionary<string, AttributeValue>()
                        {
                            {":groupNameValue", new AttributeValue {S = groupName} }
                        };
                        var expression = "SET #GN = :groupNameValue";
                        var request = awsClient.makeUpdateRequest("fitfam-mobilehub-2083376203-groups", key, attrNames, attrVals, expression);
                        client.UpdateItemAsync(request);
                    }                
                }
            }
        }
        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                using (AWSClient awsClient = new AWSClient(Amazon.RegionEndpoint.USEast1))
                {
                    using (var client = awsClient.getDynamoDBClient())
                    {
                        var key = new Dictionary<string, AttributeValue>() { { "description", new AttributeValue { S = groupId } } };
                        var attrNames = new Dictionary<string, string>()
                        {
                            {"#D", "description" }
                        };
                        var attrVals = new Dictionary<string, AttributeValue>()
                        {
                            {":descriptionValue", new AttributeValue {S = groupName} }
                        };
                        var expression = "SET #D = :descriptionValue";
                        var request = awsClient.makeUpdateRequest("fitfam-mobilehub-2083376203-groups", key, attrNames, attrVals, expression);
                        client.UpdateItemAsync(request);
                    }
                }
            }
        }

        private List<string> tags = new List<string>();
        public List<string> Tags
        {
            get { return tags; }
        }
        public void addTag(string tag)
        {
            string expression;
            if (tags.Count == 0)
            {
                expression = "SET #T = :newTag";
                tags.Add(tag);
                AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
                Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
                var request = new UpdateItemRequest
                {
                    TableName = "fitfam-mobilehub-2083376203-groups",
                    Key = new Dictionary<string, AttributeValue>() { { "groupId", new AttributeValue { S = groupId } } },
                    ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#T", "tags"},  // attribute to be updated
                },
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":newTag",new AttributeValue { L = new List<AttributeValue>() { new AttributeValue { S = tag } } }},  // new activity to update user's activities with 
                },
                    // activity added to list in database entry
                    UpdateExpression = expression
                };
                var response = dbclient.UpdateItemAsync(request);
            }
            else
            {
                expression = "ADD #T  :newTag";
                tags.Add(tag);
                AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
                Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
                var request = new UpdateItemRequest
                {
                    TableName = "fitfam-mobilehub-2083376203-groups",
                    Key = new Dictionary<string, AttributeValue>() { { "groupId", new AttributeValue { S = groupId } } },
                    ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#T", "tags"},  // attribute to be updated
                },
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":newTag",new AttributeValue { S = tag }},  // new activity to update user's activities with 
                },
                    // activity added to list in database entry
                    UpdateExpression = expression
                };
                var response = dbclient.UpdateItemAsync(request);
            }
           
        }
        public void removeTag(string tag)
        {
            tags.Remove(tag);
            AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
            Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
            var request = new UpdateItemRequest
            {
                TableName = "fitfam-mobilehub-2083376203-groups",
                Key = new Dictionary<string, AttributeValue>() { { "groupId", new AttributeValue { S = groupId } } },
                ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#T", "tags"},  // attribute to be updated
                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":oldTag",new AttributeValue { S = tag }},  // new activity to update user's activities with 
                },
                // activity added to list in database entry
                UpdateExpression = "DELETE #T :oldTag"
            };
            var response = dbclient.UpdateItemAsync(request);
        }

        private string pic;
        public string Pic
        {
            get { return pic; }
            set
            {
                pic = value;
                using (AWSClient awsClient = new AWSClient(Amazon.RegionEndpoint.USEast1))
                {
                    using (var client = awsClient.getDynamoDBClient())
                    {
                        var key = new Dictionary<string, AttributeValue>() { { "pic", new AttributeValue { S = groupId } } };
                        var attrNames = new Dictionary<string, string>()
                        {
                            {"#P", "pic" }
                        };
                        var attrVals = new Dictionary<string, AttributeValue>()
                        {
                            {":picValue", new AttributeValue {S = groupName} }
                        };
                        var expression = "SET #P = :picValue";
                        var request = awsClient.makeUpdateRequest("fitfam-mobilehub-2083376203-groups", key, attrNames, attrVals, expression);
                        client.UpdateItemAsync(request);
                    }
                }
            }
        }
        private Dictionary<User, bool> members = new Dictionary<User, bool>(); //true means they are an admin
        public Dictionary<User, bool> Members
        {
            get { return members; }
        }
        private List<User> joinRequests = new List<User>();
        public List<User> JoinRequests
        {
            get { return joinRequests; }
        }
        public async void addJoinRequest(User user)
        {
            string expression;
            if (joinRequests.Count == 0)
            {
                expression = "SET #JR = :newRequest";
                joinRequests.Add(user);
                AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
                Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
                var request = new UpdateItemRequest
                {
                    TableName = "fitfam-mobilehub-2083376203-groups",
                    Key = new Dictionary<string, AttributeValue>() { { "groupId", new AttributeValue { S = groupId } } },
                    ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#JR", "joinRequests"},  // attribute to be updated
                },
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":newRequest", new AttributeValue { L = new List<AttributeValue> { new AttributeValue{ S = user.UserId } } } } // new activity to update user's activities with 
                },

                    // activity added to list in database entry
                    UpdateExpression = expression
                };
                var response = dbclient.UpdateItemAsync(request);
            }
            else
            {
                expression = "ADD #JR :newRequest";
                joinRequests.Add(user);
                AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
                Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
                var request = new UpdateItemRequest
                {
                    TableName = "fitfam-mobilehub-2083376203-groups",
                    Key = new Dictionary<string, AttributeValue>() { { "groupId", new AttributeValue { S = groupId } } },
                    ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#JR", "joinRequests"},  // attribute to be updated
                },
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":newRequest", new AttributeValue { S = user.UserId } } // new activity to update user's activities with 
                },

                    // activity added to list in database entry
                    UpdateExpression = expression
                };
                var response = dbclient.UpdateItemAsync(request);
            }
            
        }
        private List<Event> eventList = new List<Event>();
        public List<Event> EventList
        {
            get { return eventList; }
        }
        private double boost;
        public double Boost
        {
            set
            {
                boost = value;
                // create update request to update event start time in database
                AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
                Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
                var request = new UpdateItemRequest
                {
                    TableName = "fitfam-mobilehub-2083376203-groups",
                    Key = new Dictionary<string, AttributeValue>() { { "groupId", new AttributeValue { S = groupId } } },
                    ExpressionAttributeNames = new Dictionary<string, string>()
                    {
                        {"#B", "boost"},  // attribute to be updated (event start time)
                    },
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                    {
                        {":newBoost",new AttributeValue { N = boost.ToString() }},  // new event start time
                    },

                    // expression to update event start time in database entry
                    UpdateExpression = "SET #B = :newBoost"
                };
                var response = dbclient.UpdateItemAsync(request);
            }
            get { return boost; }
        }

        private List<string> experienceLevel = new List<string>();
        public List<string> ExperienceLevel
        {
            get { return experienceLevel; }
        }
        public void addExperienceLevel(string level)
        {
            string expression;
            if (experienceLevel.Count == 0)
            {
                expression = "SET #EL = :newLevel";
                experienceLevel.Add(level);
                AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
                Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
                var request = new UpdateItemRequest
                {
                    TableName = "fitfam-mobilehub-2083376203-groups",
                    Key = new Dictionary<string, AttributeValue>() { { "groupId", new AttributeValue { S = groupId } } },
                    ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#EL", "experienceLevel"},  // attribute to be updated
                },
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":newLevel", new AttributeValue { L = new List<AttributeValue> { new AttributeValue { S = level } } } }  
                },

                    // activity added to list in database entry
                    UpdateExpression = expression
                };
                var response = dbclient.UpdateItemAsync(request);
            }
            else
            {
                expression = "ADD #EL :newLevel";
                experienceLevel.Add(level);
                AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
                Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
                var request = new UpdateItemRequest
                {
                    TableName = "fitfam-mobilehub-2083376203-groups",
                    Key = new Dictionary<string, AttributeValue>() { { "groupId", new AttributeValue { S = groupId } } },
                    ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#EL", "experienceLevel"},  // attribute to be updated
                },
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":newLevel", new AttributeValue { S = level } } // new activity to update user's activities with 
                },

                    // activity added to list in database entry
                    UpdateExpression = expression
                };
                var response = dbclient.UpdateItemAsync(request);
            }
           
        }

        public void removeExperienceLevel(string level)
        {
            experienceLevel.Remove(level);
            AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
            Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
            var request = new UpdateItemRequest
            {
                TableName = "fitfam-mobilehub-2083376203-groups",
                Key = new Dictionary<string, AttributeValue>() { { "groupId", new AttributeValue { S = groupId } } },
                ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#EL", "experienceLevel"},  // attribute to be updated
                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":oldLevel", new AttributeValue { S = level } } 
                },

                // level removed from list in database entry
                UpdateExpression = "DELETE #EL :oldLevel"
            };
            var response = dbclient.UpdateItemAsync(request);
        }

        public Group(string groupId)
        {
            this.groupId = groupId;
        }

        public async void setValues()
        {
            using (var awsClient = new AWSClient(Amazon.RegionEndpoint.USEast1))
            {
                using (var client = awsClient.getDynamoDBClient())
                {
                    var key = new Dictionary<string, AttributeValue>() { { "groupId", new AttributeValue { S = groupId } } };
                    var request = awsClient.makeGetRequest("fitfam-mobilehub-2083376203-groups", key);
                    Dictionary<string, AttributeValue> groupInfo = new Dictionary<string, AttributeValue>();
                    var task = await awsClient.GetItemAsync(client, request);
                    groupInfo = task;
                    foreach (KeyValuePair<string, AttributeValue> kvp in groupInfo)
                    {
                        switch (kvp.Key)
                        {
                            case "description":
                                description = kvp.Value.S;
                                break;
                            case "eventList":
                                var eventIdList = kvp.Value.L;
                                foreach (var eventId in eventIdList)
                                {
                                    eventList.Add(new Event(eventId.S));
                                }
                                break;
                            case "experienceLevel":
                                foreach (var item in kvp.Value.L)
                                {
                                    experienceLevel.Add(item.S);
                                }
                                break;
                            case "groupName":
                                groupName = kvp.Value.S;
                                break;
                            case "members":
                                var membersMap = kvp.Value.M;
                                foreach (var keyVal in membersMap)
                                {
                                    members.Add(new User(keyVal.Key,false), keyVal.Value.BOOL);
                                }
                                break;
                            case "pic":
                                pic = kvp.Value.S;
                                break;
                            case "tags":
                                foreach (var item in kvp.Value.L)
                                {
                                    tags.Add(item.S);
                                }
                                break;
                            case "boost":
                                boost = Convert.ToDouble(kvp.Value.N);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        public Group(string name, string description, User creator, Dictionary<User, bool> members, List<string> tags, double boost)
        {
            this.groupName = name;
            this.description = description;
            this.members = new Dictionary<User, bool>(members);
            this.tags = new List<string>(tags);
            this.boost = boost;
            groupId = creator.UserId + GroupName;
            creatorId = creator.UserId;
            writeGroup();
            /* if (awsClient.GetItemAsync(client, request) == null)
             {

             }
             else
             {
                 Console.WriteLine("Choose a new group name");
             }*/
            /*
            Dictionary<string, AttributeValue> item = new Dictionary<string, AttributeValue>()
            {
                { "groupId", new AttributeValue { S = groupId} },
                { "groupName", new AttributeValue { S = groupName } },
                { "description", new AttributeValue { S = description } },
                //{ "members", new AttributeValue { M = } }

            };
            Dictionary<string, AttributeValue> key = new Dictionary<string, AttributeValue>();
            key.Add("groupId", new AttributeValue { S = groupId });
            var request = awsClient.makeGetRequest("fitfam-mobilehub-2083376203-groups", key);
            if (awsClient.GetItemAsync(client, request) == null)
            {
                awsClient.putItem(client, awsClient.makePutRequest("fitfam-mobilehub-2083376203-groups", item));
            }
            else
            {
                Console.WriteLine("Choose a new group name");
            }
            */

        }

        private async void writeGroup()
        {
            try
            {
                System.Console.WriteLine("connecting to database");
                using (var awsClient = new AWSClient(Amazon.RegionEndpoint.USEast1))
                {
                    System.Console.WriteLine("connected to database");
                    using (var client = awsClient.getDynamoDBClient())
                    {
                        System.Console.WriteLine("writing to database");
                        var table = awsClient.getTable(client, "fitfam-mobilehub-2083376203-groups");


                        var groupEntry = new Document();
                        groupEntry["groupId"] = groupId;
                        groupEntry["groupName"] = groupName;
                        groupEntry["description"] = description;
                        var membersDoc = new Document();
                        membersDoc[creatorId] = true;
                        groupEntry["members"] = membersDoc;
                     //   groupEntry["tags"] = tags;
                        Dictionary<string, AttributeValue> key = new Dictionary<string, AttributeValue>();
                        key.Add("groupId", new AttributeValue { S = groupId });

                        var request = awsClient.makeGetRequest("fitfam-mobilehub-2083376203-groups", key);
                        var response = await table.PutItemAsync(groupEntry);
                        System.Console.WriteLine("wrote to database");

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION: {0}\n{1}", ex.Message, ex.ToString());
            }
       
        }

        public void addMember(User user, bool isAdmin)
        {
            members.Add(user, isAdmin);
            AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
            Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
            var request = new UpdateItemRequest
            {
                TableName = "fitfam-mobilehub-2083376203-groups",
                Key = new Dictionary<string, AttributeValue>() { { "groupId", new AttributeValue { S = groupId } } },
                ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#M", "members"},  // attribute to be updated
                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":newMember", new AttributeValue { M = new Dictionary<string, AttributeValue>() { { user.UserId, new AttributeValue { BOOL = isAdmin } } } } }  // new activity to update user's activities with 
                },

                // activity added to list in database entry
                UpdateExpression = "ADD #M :newMember"
            };
            var response = dbclient.UpdateItemAsync(request);

            user.addFitFam(this);
        }

        public void removeMember(User user)
        {
            members.Remove(user);
            AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
            Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
            var request = new UpdateItemRequest
            {
                TableName = "fitfam-mobilehub-2083376203-groups",
                Key = new Dictionary<string, AttributeValue>() { { "groupId", new AttributeValue { S = groupId } } },
                ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#M", "members"},  // attribute to be updated
                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":oldMember", new AttributeValue { S = user.UserId }  }  // new activity to update user's activities with 
                },

                // activity added to list in database entry
                UpdateExpression = "DELETE #M :newMember"
            };
            var response = dbclient.UpdateItemAsync(request);

            user.removeFitFam(this);
        }

        public void acceptJoinRequest(User user)
        {
            joinRequests.Remove(user);
            addMember(user, false);
            user.addFitFam(this);
        }

        public void rejectJoinRequest(User user)
        {
            joinRequests.Remove(user);
        }

        public void makeAdmin(User user)
        {
            removeMember(user);
            addMember(user, true);
        }

        public void removeAdmin(User user)
        {
            removeMember(user);
            addMember(user, false);
        }

        public void makeEvent(Event newEvent)
        {
            eventList.Add(newEvent);
            foreach (var member in members.Keys)
            {
                member.addSharedEvent(newEvent);
            }
        }

        public static async void editDescription(string groupId, string newDescription)
        {
            AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
            Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
            var request = new UpdateItemRequest
            {
                TableName = "fitfam-mobilehub-2083376203-groups",
                Key = new Dictionary<string, AttributeValue>() { { "groupId", new AttributeValue { S = groupId } } },
                ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#D", "description"},  // attribute to be updated
                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":newDescription", new AttributeValue { S = newDescription }  }  // new description to update group's description with 
                },

                // description changed in database entry
                UpdateExpression = "SET #D :newDescription"
            };
            var response = await dbclient.UpdateItemAsync(request);
        }
    }
}