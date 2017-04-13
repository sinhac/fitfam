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
using Amazon;

namespace fitfam
{ 
    class Event
    {
        private string eventId;
        public string EventId
        {
            get { return eventId; }
        }
        private string eventName;
        public string EventName
        {
            get { return eventName; }
            set { eventName = value; }
        }
        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private List<string> tags;
        private string location;
        public string Location
        {
            get { return location; }
            set { location = value; }
        }
        private DateTime startTime; //figure out recurring events
        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }
        private DateTime endTime;
        public DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }
        private bool publicEvent;
        public bool PublicEvent
        {
            get { return publicEvent; }
            set { publicEvent = value; }
        }
        private User creator;
        public User Creator
        {
            get { return creator; }
        }
        private List<User> attending;
        public List<User> Attending
        {
            get { return attending; }
        }

        public Event(string name, string description, string location, DateTime startTime, DateTime endTime, bool publicEvent, List<string> tags, User creator)
        {
            this.eventName = name;
            this.description = description;
            this.location = location;
            this.startTime = startTime;
            this.endTime = endTime;
            this.publicEvent = publicEvent;
            this.tags.AddRange(tags);
            this.creator = creator;
            this.addAttending(creator);

            using (var awsClient = new AWSClient(Amazon.RegionEndpoint.USEast1))
            {
                using (var client = awsClient.getDynamoDBClient())
                {
                    try
                    {
                        eventId = eventName + creator.UserId + startTime.ToString();
                        // create list of userids from list of attending Users
                        List<string> attending_userids = new List<string>();
                        for (int i = 0; i < attending.Count; i++)
                        {
                            attending_userids.Add(attending[i].UserId);
                        }
                        Dictionary<string, AttributeValue> item = new Dictionary<string, AttributeValue>()
                    {
                        { "eventId", new AttributeValue { S = eventId} },
                        { "eventName", new AttributeValue { S = eventName } },
                        { "description", new AttributeValue { S = description } },
                        { "location", new AttributeValue { S = location } },
                        { "startTime", new AttributeValue { S = startTime.ToString() } },
                        { "endTime", new AttributeValue { S = endTime.ToString() } },
                        { "publicEvent", new AttributeValue { BOOL = publicEvent } },
                        { "tags", new AttributeValue { SS = tags } },
                        { "attending", new AttributeValue { SS = attending_userids } }
                    };
                        awsClient.putItem(client, awsClient.makePutRequest("fitfam-mobilehub-2083376203-events", item));
                        System.Console.WriteLine("ADDED NEW EVENT");
                    } catch (Exception e)
                    {
                        if (e is ResourceNotFoundException)
                        {
                            System.Console.WriteLine("REsOURCENOTFOUND");
                        } else if (e is InternalServerErrorException)
                        {
                            System.Console.WriteLine("INTERNALSERVERERROREXCEPTION");
                        } else if (e is ConditionalCheckFailedException)
                        {
                            System.Console.WriteLine("CONDITIONALCHECKFAILED");
                        }
                    }
                   
                }
            }
        }

        public void addAttending(User attendingUser)
        {
            attending.Add(attendingUser);
            // create request to add attending user's userId to list in database
            AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
            Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
            var request = new UpdateItemRequest
            {
                TableName = "fitfam-mobilehub-2083376203-events",
                Key = new Dictionary<string, AttributeValue>() { { "eventId", new AttributeValue { S = eventName + creator.UserId + startTime.ToString() } } },
                ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#A", "attending"},  // attribute to be updated
                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":newAttending",new AttributeValue { S = attendingUser.UserId }},  // userId to be added to list of attending
                },

                // expression to add user's id to "attending" list in database entry
                UpdateExpression = "ADD #A :newAttending"
            };
            var response = dbclient.UpdateItemAsync(request);

            //// Check the response.
            //var attributeList = response.Attributes; // attribute list in the response.
            //                                         // print attributeList.
            //Console.WriteLine("\nPrinting item after 'attending' attribute update ............");
            //PrintItem(attributeList);
        }

        public void addTag(string tag)
        {
            tags.Add(tag);

            // create request to add event tag to tags list in database
            AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
            Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
            var request = new UpdateItemRequest
            {
                TableName = "fitfam-mobilehub-2083376203-events",
                Key = new Dictionary<string, AttributeValue>() { { "eventId", new AttributeValue { S = eventName + creator.UserId + startTime.ToString() } } },
                ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#T", "tags"},  // attribute to be updated
                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":newTag",new AttributeValue { S = tag }},  // tag to be added to list of tags
                },

                // expression to add tag to "tags" list in database entry
                UpdateExpression = "ADD #T :newTag"
            };
            var response = dbclient.UpdateItemAsync(request);
            // TO-DO: error-check response
        }


        public void removeAttending(User attendingUser)
        {
            attending.Remove(attendingUser);

            // create request to remove attending user's userId from list in database
            AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
            Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
            var request = new UpdateItemRequest
            {
                TableName = "fitfam-mobilehub-2083376203-events",
                Key = new Dictionary<string, AttributeValue>() { { "eventId", new AttributeValue { S = eventName + creator.UserId + startTime.ToString() } } },
                ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#A", "attending"},  // attribute to be updated
                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":notAttending",new AttributeValue { S = attendingUser.UserId }},  // userId to be removed from list of attending
                },

                // expression to remove user's id from "attending" list in database entry
                UpdateExpression = "DELETE #A :notAttending"
            };
            var response = dbclient.UpdateItemAsync(request);
            // TO-DO: error-check response
        }

        public void removeTag(string tag)
        {
            tags.Remove(tag);

            // create request to remove event tag from tags list in database
            AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
            Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
            var request = new UpdateItemRequest
            {
                TableName = "fitfam-mobilehub-2083376203-events",
                Key = new Dictionary<string, AttributeValue>() { { "eventId", new AttributeValue { S = eventName + creator.UserId + startTime.ToString() } } },
                ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#T", "tags"},  // attribute to be updated
                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":tagToRemove",new AttributeValue { S = tag }},  // tag to be removed from list of tags
                },

                // expression to remove tag from "tags" list in database entry
                UpdateExpression = "DELETE #T :tagToRemove"
            };
            var response = dbclient.UpdateItemAsync(request);
            // TO-DO: error-check response
        }

    }
}