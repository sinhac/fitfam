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
using Amazon.DynamoDBv2;

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
            set
            {
                // update locally
                eventName = value;
                // create update request to update event name in database
                AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
                Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
                var request = new UpdateItemRequest
                {
                    TableName = "fitfam-mobilehub-2083376203-events",
                    Key = new Dictionary<string, AttributeValue>() { { "eventId", new AttributeValue { S = eventId } } },
                    ExpressionAttributeNames = new Dictionary<string, string>()
                    {
                        {"#E", "eventName"},  // attribute to be updated (event name)
                    },
                        ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                    {
                        {":newEventName",new AttributeValue { S = eventName }},  // new event name
                    },

                    // expression to update event name in database entry
                    UpdateExpression = "SET #E :newEventName"
                };
                var response = dbclient.UpdateItemAsync(request);
            }
        }
        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                // update locally
                description = value;
                // create update request to update event description in database
                AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
                Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
                var request = new UpdateItemRequest
                {
                    TableName = "fitfam-mobilehub-2083376203-events",
                    Key = new Dictionary<string, AttributeValue>() { { "eventId", new AttributeValue { S = eventId } } },
                    ExpressionAttributeNames = new Dictionary<string, string>()
                    {
                        {"#D", "description"},  // attribute to be updated (event description)
                    },
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                    {
                        {":newEventDescription",new AttributeValue { S = description }},  // new event description
                    },

                    // expression to update event description in database entry
                    UpdateExpression = "SET #E :newEventDescription"
                };
                var response = dbclient.UpdateItemAsync(request);
            }
        }
        private List<string> tags = new List<string>();
        private string location;
        public string Location
        {
            get { return location; }
            set
            {
                // update locally
                location = value;
                // create update request to update event location in database
                AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
                Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
                var request = new UpdateItemRequest
                {
                    TableName = "fitfam-mobilehub-2083376203-events",
                    Key = new Dictionary<string, AttributeValue>() { { "eventId", new AttributeValue { S = eventId } } },
                    ExpressionAttributeNames = new Dictionary<string, string>()
                    {
                        {"#L", "location"},  // attribute to be updated (event location)
                    },
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                    {
                        {":newEventLocation",new AttributeValue { S = location }},  // new event location
                    },

                    // expression to update event location in database entry
                    UpdateExpression = "SET #L :newEventLocation"
                };
                var response = dbclient.UpdateItemAsync(request);
            }
        }
        private DateTime startTime; //figure out recurring events
        public DateTime StartTime
        {
            get { return startTime; }
            set
            {
                // update locally
                startTime = value;
                // create update request to update event start time in database
                AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
                Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
                var request = new UpdateItemRequest
                {
                    TableName = "fitfam-mobilehub-2083376203-events",
                    Key = new Dictionary<string, AttributeValue>() { { "eventId", new AttributeValue { S = eventId } } },
                    ExpressionAttributeNames = new Dictionary<string, string>()
                    {
                        {"#S", "startTime"},  // attribute to be updated (event start time)
                    },
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                    {
                        {":newEventStart",new AttributeValue { S = startTime.ToString() }},  // new event start time
                    },

                    // expression to update event start time in database entry
                    UpdateExpression = "SET #S :newEventStart"
                };
                var response = dbclient.UpdateItemAsync(request);
            }
        }
        private DateTime endTime;
        public DateTime EndTime
        {
            get { return endTime; }
            set
            {
                // update locally
                endTime = value;
                // create update request to update event end time in database
                AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
                Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
                var request = new UpdateItemRequest
                {
                    TableName = "fitfam-mobilehub-2083376203-events",
                    Key = new Dictionary<string, AttributeValue>() { { "eventId", new AttributeValue { S = eventId } } },
                    ExpressionAttributeNames = new Dictionary<string, string>()
                    {
                        {"#T", "endTime"},  // attribute to be updated (event end time)
                    },
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                    {
                        {":newEventEnd",new AttributeValue { S = endTime.ToString() }},  // new event end time
                    },

                    // expression to update event start time in database entry
                    UpdateExpression = "SET #T :newEventEnd"
                };
                var response = dbclient.UpdateItemAsync(request);
            }
        }
        private bool publicEvent;
        public bool PublicEvent
        {
            get { return publicEvent; }
            set
            {
                // update locally
                publicEvent = value;
                // create update request to update event public status in database
                AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
                Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
                var request = new UpdateItemRequest
                {
                    TableName = "fitfam-mobilehub-2083376203-events",
                    Key = new Dictionary<string, AttributeValue>() { { "eventId", new AttributeValue { S = eventId } } },
                    ExpressionAttributeNames = new Dictionary<string, string>()
                    {
                        {"#P", "publicEvent"},  // attribute to be updated (event public status)
                    },
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                    {
                        {":newStatus",new AttributeValue { BOOL = publicEvent }},  // new event status
                    },

                    // expression to update event status in database entry
                    UpdateExpression = "SET #P :newStatus"
                };
                var response = dbclient.UpdateItemAsync(request);
            }
        }
        private User creator;
        public User Creator
        {
            get { return creator; }
        }
        private List<User> attending = new List<User>();
        public List<User> Attending
        {
            get { return attending; }
        }
        private List<string> shared = new List<string>();
        public List<string> Shared
        {
            get { return shared; }
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
                    eventId = eventName + creator.UserId + startTime.ToString();
                    // create list of userids from list of attending Users
                    List<string> attending_userids = new List<string>();
                   
                    Dictionary<string, AttributeValue> item = new Dictionary<string, AttributeValue>()
                    {
                        { "eventId", new AttributeValue { S = eventId} },
                        { "eventName", new AttributeValue { S = eventName } },
                        { "description", new AttributeValue { S = description } },
                        { "location", new AttributeValue { S = location } },
                        //{ "startTime", new AttributeValue { S = startTime.ToString() } },
                        //{ "endTime", new AttributeValue { S = endTime.ToString() } },
                        //{ "publicEvent", new AttributeValue { BOOL = publicEvent } },
                        //{ "tags", new AttributeValue { SS = tags } },
                        //{ "attending", new AttributeValue { SS = attending_userids } }
                    };
                    awsClient.putItem(client, awsClient.makePutRequest("fitfam-mobilehub-2083376203-events", item));
                }
            }
        }

        public Event(string eventId)
        {
            //pull info 
            AWSClient client = new AWSClient(Amazon.RegionEndpoint.USEast1);
            AmazonDynamoDBClient dbclient = client.getDynamoDBClient();
            string tableName = "fitfam-mobilehub-2083376203-events";
            var Key = new Dictionary<string, AttributeValue>() { { "eventId", new AttributeValue { S = eventId } } };
            Dictionary<string, AttributeValue> eventInfo = new Dictionary<string, AttributeValue>();
            var request = client.makeGetRequest(tableName, Key);
            SetValues(client, dbclient, request);
        }

        private async void SetValues(AWSClient client, AmazonDynamoDBClient dbclient, GetItemRequest request)
        {
            var eventInfo = await client.GetItemAsync(dbclient, request);
            this.eventName = eventInfo["eventName"].S;
            this.description = eventInfo["description"].S;
            this.location = eventInfo["location"].S;
            this.startTime = Convert.ToDateTime(eventInfo["startTime"].S);
            this.endTime = Convert.ToDateTime(eventInfo["endTime"].S);
            this.publicEvent = eventInfo["publicEvent"].BOOL;
            this.tags = eventInfo["tags"].SS.ToList<string>();
            this.creator = User(eventInfo["creator"], true);
            this.shared = eventInfo["shared"].SS.ToList<string>();
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