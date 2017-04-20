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
    struct FamUtil
    {
        public string groupId;
        public double util;
        public FamUtil(User user, List<string> tags, string experienceLevel, string id, List<string> eventTags, List<string> expLevels, double boost)
        {
            groupId = id;
            util = 0;
            if (expLevels.Contains(experienceLevel))
            {
                util += 1;
            }
            var matches = 0.0;
            var totalTags = tags.Count + eventTags.Count;
            if (totalTags > 0)
            {
                foreach (var tag in tags)
                {

                    if (eventTags.Contains(tag))
                    {
                        matches += 1;
                    }
                }
                util += (2 * matches) / totalTags;
            }
            if (matches > 0)
            {
                util += boost;
            }
           
        }
    }
    class FindAFam
    {
        private List<string> famSearchResults = new List<string>();
        public List<string> FamSearchResults
        {
            get { return famSearchResults; }
        }
        public FindAFam(User user, List<string> tags, string experienceLevel)
        {
            getFamUtils(user, tags, experienceLevel);

        }
        private async void getFamUtils(User user, List<string> tags, string experienceLevel)
        {
            List<FamUtil> utils = new List<FamUtil>();
            using (var awsClient = new AWSClient(Amazon.RegionEndpoint.USEast1))
            {
                using (var client = awsClient.getDynamoDBClient())
                {
                    var request = new ScanRequest
                    {
                        TableName = "fitfam-mobilehub-2083376203-groups"
                    };
                    var response = await client.ScanAsync(request);
                    var result = response.Items;

                    foreach (Dictionary<string, AttributeValue> item in result)
                    {
                        var groupId = item["groupId"].S;
                        var groupTags = item["tags"].SS.ToList();
                        var groupExperienceLevels = item["experienceLevels"].SS.ToList();
                        double boost = Convert.ToDouble(item["boost"].N);
                        FamUtil newUtil = new FamUtil(user, tags, experienceLevel, groupId, groupTags, groupExperienceLevels, boost);
                        if (newUtil.util > 1)
                        {
                            utils.Add(newUtil);
                        }
                    }
                }
            }

            // Array utilArray = utils.ToArray<FamUtil>();
            //   Array.Sort(utilArray, (a, b) => a.util.CompareTo(b.util));
            var utilsArr = utils.OrderBy(u => u.util).ToArray();
            foreach(var util in utilsArr)
            {
                famSearchResults.Add(util.groupId);
            }
        }
    }

    
}