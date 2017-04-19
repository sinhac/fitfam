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
    class FindAFam
    {
        public FindAFam(User user, List<string> tags)
        {

        }
        private async void getFamUtils(List<string> tags, string location, string experience)
        {
            using (var awsClient = new AWSClient(Amazon.RegionEndpoint.USEast1))
            {
                using (var client = awsClient.getDynamoDBClient())
                {
                    var request = new QueryRequest
                    {
                        TableName = "fitfam-mobilehub-2083376203-tags",
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
                            // utils.Add(newUtil);
                        }

                    }


                }
            }
        }
    }

    
}