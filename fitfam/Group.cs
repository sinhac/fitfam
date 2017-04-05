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
            set { groupName = value; }
        }
        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
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
            set { pic = value; }
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
             this.addMember(creator, true);

             using (var awsClient = new AWSClient(Amazon.RegionEndpoint.USEast1))
                {
                using (var client = awsClient.getDynamoDBClient())
                {
                    using (var table = awsClient.getTable(client, "fitfam-mobilehub-2083376203-groups"))
                    {
                        groupId = groupName + creator.UserId;
                        var groupEntry = new Document();
                        groupEntry["groupId"] = groupId;
                        groupEntry["groupName"] = groupName;
                        groupEntry["description"] = description;
                        var membersDoc = new Document();
                        membersDoc[creator.userId] = true;
                        groupEntry["members"] = membersDoc;
                        table.PutItem(groupEntry);
                    }
                    /*
                    Dictionary<string, AttributeValue> item = new Dictionary<string, AttributeValue>()
                    {
                        { "groupId", new AttributeValue { S = groupId} },
                        { "groupName", new AttributeValue { S = groupName } },
                        { "description", new AttributeValue { S = description } },
                        { "members", new AttributeValue { M = } }

                    };
                    awsClient.putItem(client, awsClient.makePutRequest(, item));
                    */
                }
             }
            
        }

        public void addMember(User user, bool isAdmin)
        {
            members[user] = isAdmin;
            //add member to server
        }


    }
}