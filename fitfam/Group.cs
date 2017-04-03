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

        public Group(string name, string description)
        {
             this.groupName = name;
             this.description = description;
             using (var awsClient = new AWSClient(Amazon.RegionEndpoint.USEast1))
                {
                using (var client = awsClient.getDynamoDBClient())
                {
                    Dictionary<string, AttributeValue> item = new Dictionary<string, AttributeValue>()
                    {
                        { "groupName", new AttributeValue { S = groupName } },
                        { "description", new AttributeValue { S = description } }
                    };
                    awsClient.putItem(client, awsClient.makePutRequest("fitfam-mobilehub-2083376203-groups", item));
                
                }
             }
            
        }


    }
}