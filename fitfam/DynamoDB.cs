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
using Amazon.DynamoDBv2;

namespace fitfam
{
    class DynamoDB
    {
        AmazonDynamoDBClient client;
        public DynamoDB(string awsAccessKeyId, string awsSecretAccessKey, Amazon.RegionEndpoint region)
        {
            client = new AmazonDynamoDBClient(awsAccessKeyId, awsSecretAccessKey, region);
        }

    }
}