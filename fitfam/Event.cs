using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;

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
            }
            else
            {
                expression = "ADD #T  :newTag";
            }
            tags.Add(tag);
            AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
            Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
            var request = new UpdateItemRequest
            {
                TableName = "fitfam-mobilehub-2083376203-events",
                Key = new Dictionary<string, AttributeValue>() { { "eventId", new AttributeValue { S = eventId } } },
                ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#T", "tags"},  // attribute to be updated
                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":newTag",new AttributeValue { S = tag }},  // new activity to add to event tags 
                },
                // activity added to list in database entry
                UpdateExpression = expression
            };
            var response = dbclient.UpdateItemAsync(request);
        }
        public void removeTag(string tag)
        {       
            tags.Remove(tag);
            AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
            Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
            var request = new UpdateItemRequest
            {
                TableName = "fitfam-mobilehub-2083376203-events",
                Key = new Dictionary<string, AttributeValue>() { { "eventId", new AttributeValue { S = eventId } } },
                ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#T", "tags"},  // attribute to be updated
                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":oldTag",new AttributeValue { S = tag }},  // new tag to remove from event tags  
                },
                // tag removed from list in database entry
                UpdateExpression = "DELETE #T :oldTag"
            };
            var response = dbclient.UpdateItemAsync(request);
        }
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
                    UpdateExpression = "SET #L = :newEventLocation"
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
                    UpdateExpression = "SET #S = :newEventStart"
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
                    UpdateExpression = "SET #T = :newEventEnd"
                };
                var response = dbclient.UpdateItemAsync(request);
            }
        }
        private bool publicEvent = false;
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
                    UpdateExpression = "SET #P = :newStatus"
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
        private List<User> shared = new List<User>();
        public List<User> Shared
        {
            get { return shared; }
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
                    TableName = "fitfam-mobilehub-2083376203-events",
                    Key = new Dictionary<string, AttributeValue>() { { "eventId", new AttributeValue { S = eventId } } },
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
       /* private List<string> experienceLevels = new List<string>();
        public List<string> ExperienceLevels
        {
            get { return experienceLevels; }
        }
        public void addExperienceLevel(string experienceLevel)
        {
            string expression;
            if (experienceLevels.Count == 0)
            {
                expression = "SET #T = :newExperienceLevel";
            }
            else
            {
                expression = "ADD #T  :newExperienceLevel";
            }
            experienceLevels.Add(experienceLevel);
            AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
            Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
            var request = new UpdateItemRequest
            {
                TableName = "fitfam-mobilehub-2083376203-events",
                Key = new Dictionary<string, AttributeValue>() { { "eventId", new AttributeValue { S = eventId } } },
                ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#T", "experienceLevels"},  // attribute to be updated
                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":newExperienceLevel",new AttributeValue { S = experienceLevel }},  // new activity to add to event tags 
                },
                // activity added to list in database entry
                UpdateExpression = expression
            };
            var response = dbclient.UpdateItemAsync(request);
        }*/

        public Event(string name, string description, string location, DateTime startTime, DateTime endTime, bool publicEvent, List<string> tags, User creator, double boost)
        {
            this.eventName = name;
            this.description = description;
            this.location = location;
            this.startTime = startTime;
            this.endTime = endTime;
            this.publicEvent = publicEvent;
            this.tags = new List<string>(tags);
            //this.experienceLevels = new List<string>(experienceLevels);
            this.creator = creator;
            this.addAttending(creator);
            this.boost = boost;
            eventId = eventName + creator.UserId + startTime.ToString();
            writeEvent();
        }

        private async void writeEvent()
        {
            try { 
                using (var awsClient = new AWSClient(Amazon.RegionEndpoint.USEast1))
                {
                using (var client = awsClient.getDynamoDBClient())
                {
/*eventId = creator.UserId + eventName + startTime.ToString();
// create list of userids from list of attending Users
List<string> attending_userids = new List<string>();
attending_userids.Add(creator.UserId);
this.tags.Add("test");
Dictionary<string, AttributeValue> item = new Dictionary<string, AttributeValue>()
{
    { "eventId", new AttributeValue { S = eventId} },
    { "eventName", new AttributeValue { S = eventName } },
    { "description", new AttributeValue { S = description } },
    { "location", new AttributeValue { S = location } },
    { "startTime", new AttributeValue { S = startTime.ToString() } },
    { "endTime", new AttributeValue { S = endTime.ToString() } },
    { "publicEvent", new AttributeValue { BOOL = publicEvent } },
    { "tags", new AttributeValue { SS = this.tags } },
    { "attending", new AttributeValue { SS = attending_userids } }
};
awsClient.putItem(client, awsClient.makePutRequest("fitfam-mobilehub-2083376203-events", item));*/
            try
            {
                            var table = awsClient.getTable(client, "fitfam-mobilehub-2083376203-events");
                            var eventEntry = new Document();
                            eventEntry["eventId"] = eventId;
                            eventEntry["eventName"] = eventName;
                            eventEntry["description"] = description;
                            var attendingDoc = new Document();
                            attendingDoc[Creator.UserId] = true;
                            eventEntry["attending"] = attendingDoc;
                            eventEntry["tags"] = tags;
                            //eventEntry["experienceLevels"] = experienceLevels;
                            eventEntry["startTime"] = startTime.ToString();
                            eventEntry["endTime"] = endTime.ToString();
                            eventEntry["location"] = location;
                            Dictionary<string, AttributeValue> key = new Dictionary<string, AttributeValue>();
                            key.Add("eventId", new AttributeValue { S = eventId });
                            var request = awsClient.makeGetRequest("fitfam-mobilehub-2083376203-event", key);
                            var response = await table.PutItemAsync(eventEntry);
                            System.Console.WriteLine("wrote to database");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Event Exception: {0}\n{1}", ex.Message, ex.Source);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION: {0}\n{1}", ex.Message, ex.ToString());
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
            this.tags = eventInfo["tags"].SS;
            this.creator = new User(eventInfo["creator"].S, true);
            var sharedList = eventInfo["shared"].SS.ToList<string>();
            foreach (var userId in sharedList)
            {
                shared.Add(new User(userId, false));
            }
            var attendingList = eventInfo["attending"].SS.ToList();
            foreach (var userId in attendingList)
            {
                attending.Add(new User(userId, false));
            }
        }

        public void addAttending(User attendingUser)
        {
            String expression;
            if (attending.Count == 0)
            {
                expression = "SET #A = :newAttending";
            }
            else
            {
                expression = "ADD #A :newAttending";
            }
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
                UpdateExpression = expression
            };
            var response = dbclient.UpdateItemAsync(request);
            System.Console.WriteLine("finished add attending");
            attendingUser.addJoinedEvent(this);
        }

        public void addShared(User sharedUser)
        {
            String expression;
            if (attending.Count == 0)
            {
                expression = "SET #S = :newShared";
            }
            else
            {
                expression = "ADD #S :newShared";
            }
            shared.Add(sharedUser);

            AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
            Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
            var request = new UpdateItemRequest
            {
                TableName = "fitfam-mobilehub-2083376203-events",
                Key = new Dictionary<string, AttributeValue>() { { "eventId", new AttributeValue { S = eventName + creator.UserId + startTime.ToString() } } },
                ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#S", "shared"},  // attribute to be updated
                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":newShared",new AttributeValue { S = sharedUser.UserId }},  
                },

                UpdateExpression = expression
            };
            var response = dbclient.UpdateItemAsync(request);
            System.Console.WriteLine("finished add shared");
            sharedUser.addSharedEvent(this);
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
            attendingUser.removeJoinedEvent(this);
        }

        public void removeShared(User sharedUser)
        {
            shared.Remove(sharedUser);

            // create request to remove attending user's userId from list in database
            AWSClient awsclient = new AWSClient(Amazon.RegionEndpoint.USEast1);
            Amazon.DynamoDBv2.AmazonDynamoDBClient dbclient = awsclient.getDynamoDBClient();
            var request = new UpdateItemRequest
            {
                TableName = "fitfam-mobilehub-2083376203-events",
                Key = new Dictionary<string, AttributeValue>() { { "eventId", new AttributeValue { S = eventName + creator.UserId + startTime.ToString() } } },
                ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#S", "shared"},  // attribute to be updated
                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":notShared",new AttributeValue { S = sharedUser.UserId }},  // userId to be removed from list of attending
                },

                // expression to remove user's id from "attending" list in database entry
                UpdateExpression = "DELETE #S :notShared"
            };
            var response = dbclient.UpdateItemAsync(request);
            sharedUser.removeJoinedEvent(this);
        }

      

        public void Delete()
        {
            foreach (var user in shared)
            {
                user.removeSharedEvent(this);
            }
            foreach (var user in attending)
            {
                user.removeJoinedEvent(this);
            }
           /* using (var awsClient = new AWSClient(Amazon.RegionEndpoint.USEast1))
            {
                using (var client = awsClient.getDynamoDBClient())
                {
}
                    DeleteItemRequest request = new DeleteItemRequest
                    {
                        TableName = "fitfam-mobilehub-2083376203-events",
                        Key = new Dictionary<string, AttributeValue>() { HashKeyElement = new AttributeValue { S = "eventId" } }
                    };
                }
            }*/


        }

    }
}