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
    class Group
    {
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
        private List<string> tags;
        public List<string> Tags
        {
            get { return tags; }
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
        private Dictionary<User, bool> members; //true means they are an admin
        public Dictionary<User, bool> Members
        {
            get { return members; }
        }
        private List<Event> eventList;
        public List<Event> EventList
        {
            get { return eventList; }
        }

        private List<string> experienceLevels = new List<string>();
        public List<string> ExperienceLevels
        {
            get { return experienceLevels; }
        }
        public void addExperienceLevel(string level)
        {
            //do this shit later
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
                                var eventIdList = kvp.Value.SS.ToList<string>();
                                foreach (var eventId in eventIdList)
                                {
                                    eventList.Add(new Event(eventId));
                                }
                                break;
                            case "experienceLevels":
                                experienceLevels = kvp.Value.SS.ToList();
                                break;

                        }
                    }
                }
            }
        }

        public Group(string name, string description, User creator, Dictionary<User, bool> members)
        {
             this.groupName = name;
             this.description = description;
            this.members = new Dictionary<User, bool>(members);
            System.Console.WriteLine("connecting to database");
             using (var awsClient = new AWSClient(Amazon.RegionEndpoint.USEast1))
             {
                System.Console.WriteLine("connected to database");
                using (var client = awsClient.getDynamoDBClient())
                {
                    System.Console.WriteLine("writing to database");
                    var table = awsClient.getTable(client, "fitfam-mobilehub-2083376203-groups");
                    
                    groupId = creator.UserId + GroupName;
                    var groupEntry = new Document();
                    groupEntry["groupId"] = groupId;
                    groupEntry["groupName"] = groupName;
                    groupEntry["description"] = description;
                    var membersDoc = new Document();
                    membersDoc[creator.UserId] = true;
                    groupEntry["members"] = membersDoc;
                    Dictionary<string, AttributeValue> key = new Dictionary<string, AttributeValue>();
                    key.Add("groupId", new AttributeValue { S = groupId });
                    var request = awsClient.makeGetRequest("fitfam-mobilehub-2083376203-groups", key);
                    if (awsClient.GetItemAsync(client, request) == null)
                    {
                        table.PutItemAsync(groupEntry);
                        System.Console.WriteLine("wrote to database");
                    }
                    else
                    {
                        Console.WriteLine("Choose a new group name");
                    }
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
                    {"#M", ":newMember"},  // attribute to be updated
                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":newMember", new AttributeValue { M = new Dictionary<string, AttributeValue>() { { user.UserId, new AttributeValue { BOOL = isAdmin } } } } }  // new activity to update user's activities with 
                },

                // activity added to list in database entry
                UpdateExpression = "ADD #M :newMember"
            };
            var response = dbclient.UpdateItemAsync(request);
            //add member to server
        }


    }
}