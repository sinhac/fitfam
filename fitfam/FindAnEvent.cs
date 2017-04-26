using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.DynamoDBv2.Model;

namespace fitfam
{
    struct EventUtil
    {
        public string eventId;
        public int util;
        public EventUtil(string Id, List<string> tags, List<string> eventTags, string experienceLevel, List<string> eventExperienceLevels)
        {
            eventId = Id;
            util = 0;
            foreach (var tag in tags)
            {
                if (eventTags.Contains(tag))
                {
                    util += 1;
                }
            }
            if (eventExperienceLevels.Contains(experienceLevel))
            {
                util += 2;
            }
        }
    }
    class FindAnEvent
    {
        private List<EventUtil> utils = new List<EventUtil>();
        private Array matches;
        public Array Matches //get this to get the matches in order
        {
            get { return matches; }
        }
        public FindAnEvent(User user, List<string> tags, DateTime startTime, DateTime endTime)
        {
            // getEventUtils(tags, location, experience);
            var utilsArr = utils.ToArray();
            Array.Sort(utilsArr, (a, b) => a.util.CompareTo(b.util));
            Array.Copy(utilsArr, matches, utils.Count);
        }

        private async void getEventUtils(List<string> tags, string location, string experience)
        {
            using (var awsClient = new AWSClient(Amazon.RegionEndpoint.USEast1))
            {
                using (var client = awsClient.getDynamoDBClient())
                {
                    var request = new QueryRequest
                    {
                        TableName = "fitfam-mobilehub-2083376203-events",
                        KeyConditionExpression = "location = :v_location",
                        ExpressionAttributeValues = new Dictionary<string, AttributeValue> { { ":v_location", new AttributeValue { S = location } } },
                        ProjectionExpression = "eventId, tags, experienceLevels",
                        ConsistentRead = true
                    };
                    var task = await client.QueryAsync(request);
                    var response = task;
                    foreach (Dictionary<string, AttributeValue> item in response.Items)
                    {
                        var eventId = item["eventId"].S;
                        var eventTags = item["tags"].SS.ToList();
                        var experienceLevels = item["experienceLevels"].SS.ToList();
                        EventUtil newUtil = new EventUtil(eventId, tags, eventTags, experience, experienceLevels);
                        if (newUtil.util > 2)
                        {
                            utils.Add(newUtil);
                        }
                        
                    }


                }
            }
        }
    }
}