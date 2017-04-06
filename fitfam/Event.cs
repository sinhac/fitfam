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
                    eventId = eventName + creator.UserId + startTime.ToString();
                    Dictionary<string, AttributeValue> item = new Dictionary<string, AttributeValue>()
                    {
                        { "eventId", new AttributeValue { S = eventId} },
                        { "eventName", new AttributeValue { S = eventName } },
                        { "description", new AttributeValue { S = description } },
                        { "location", new AttributeValue { S = location } },
                        { "startTime", new AttributeValue { S = startTime.ToString() } },
                        { "endTime", new AttributeValue { S = endTime.ToString() } },
                        { "publicEvent", new AttributeValue { S = publicEvent } },
                        { "tags", new AttributeValue { S = tags } },
                        { "attending", new AttributeValue { S = attending } }
                    };
                    awsClient.putItem(client, awsClient.makePutRequest("fitfam-mobilehub-2083376203-events", item));

                }
            }
        }

        public void addAttending(User attendingUser)
        {
            attending.Add(attendingUser);
            // add attending user's userId to database

        }

        public void addTag(string tag)
        {
            tags.Add(tag);
            // add tag to database

        }

    }
}