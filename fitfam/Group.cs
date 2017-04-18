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

        public Group(string name, string description, User creator)
        {
             this.groupName = name;
             this.description = description;
            this.members = new Dictionary<User, bool>();
             this.addMember(creator, true);
            System.Console.WriteLine("connecting to database");
             using (var awsClient = new AWSClient(Amazon.RegionEndpoint.USEast1))
             {
                System.Console.WriteLine("connected to database");
                using (var client = awsClient.getDynamoDBClient())
                {
                    System.Console.WriteLine("writing to database");
                    var table = awsClient.getTable(client, "fitfam-mobilehub-2083376203-groups");
                    
                    groupId = groupName + creator.UserId;
                    var groupEntry = new Document();
                    groupEntry["groupId"] = groupId;
                    groupEntry["groupName"] = groupName;
                    groupEntry["description"] = description;
                    var membersDoc = new Document();
                    membersDoc[creator.UserId] = true;
                    groupEntry["members"] = membersDoc;
                    table.PutItemAsync(groupEntry);
                    System.Console.WriteLine("wrote to database");
                    
                    Dictionary<string, AttributeValue> item = new Dictionary<string, AttributeValue>()
                    {
                        { "groupId", new AttributeValue { S = groupId} },
                        { "groupName", new AttributeValue { S = groupName } },
                        { "description", new AttributeValue { S = description } },
                        //{ "members", new AttributeValue { M = } }

                    };
                    awsClient.putItem(client, awsClient.makePutRequest("fitfam-mobilehub-2083376203-groups", item));


                }
             }
            
        }

        public void addMember(User user, bool isAdmin)
        {
            members.Add(user, isAdmin);
            //add member to server
        }


    }
}