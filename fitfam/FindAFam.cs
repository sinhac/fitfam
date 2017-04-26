using System;
using System.Collections.Generic;
using System.Linq;
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
                    foreach(var eventTag in eventTags)
                    {
                        if (tag == eventTag)
                        {
                            matches += 1;

                        }
                    }

                }
                util += (2 * matches) / totalTags;
            }
            if (matches > 0)
            {
                util += boost;
            }
            Console.WriteLine("util {0}", util);
           
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
                        TableName = "fitfam-mobilehub-2083376203-groups",
                        ProjectionExpression = "groupId, tags, experienceLevels"
                    };
                    var response = await client.ScanAsync(request);
                    var result = response.Items;

                    
                    foreach (Dictionary<string, AttributeValue> item in result)
                    {
                        /*int numMatches = 0;
                        string groupId = item["groupId"].S;
                        Console.WriteLine("Idddddd " + groupId);

                        foreach( var kvp in item)
                        {
                            if(kvp.Key == "tags")
                            {
                                var groupTags = kvp.Value.SS;
                                foreach(string t in groupTags)
                                {
                                    foreach(string tag in tags)
                                    {
                                        if(t == tag) {
                                            numMatches++;
                                        }
                                    }
                                }
                            }
                            if(kvp.Key == "experienceLevel")
                            {
                                var experienceLevels = kvp.Value.SS;
                                foreach(string e in experienceLevels)
                                {
                                    if(e[0] == experienceLevel[0])
                                    {
                                        numMatches++;
                                    }
                                }
                            }
                        }*/
                        double boost = 0;
                        string groupId = item["groupId"].S;
                        var groupTags = new List<string>();
                        var groupExperienceLevels = new List<string>();
                        foreach (var kvp in item)
                        {
                            switch (kvp.Key)
                            {
                                case "tags":
                                    Console.WriteLine("Tags");
                                    Console.WriteLine(kvp.Value.SS.Count);
                                    foreach (var tag in kvp.Value.SS)
                                    {
                                        groupTags.Add(tag);
                                        Console.WriteLine(tag);
                                    }
                                    break;
                                case "experienceLevels":
                                    Console.WriteLine("levels");
                                    foreach (var level in kvp.Value.SS)
                                    {
                                        groupExperienceLevels.Add(level);
                                    }
                                    break;
                            }
                            FamUtil newUtil = new FamUtil(user, tags, experienceLevel, groupId, groupTags, groupExperienceLevels, boost);
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