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
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Android.Gms.Tasks;
using System.Threading.Tasks;

namespace fitfam
{
    class AWSClient : System.IDisposable
    {
        private Amazon.RegionEndpoint region;
        private string publicKey;
        private string privateKey;
        
        /// <summary>
        /// Initiates AWSClient class
        /// </summary>
        /// <param name="r">Region</param>
        /// <param name="pubKey">Public Key</param>
        /// <param name="privKey">Private Key</param>   
        public AWSClient(Amazon.RegionEndpoint r, string pubKey = "AKIAIJDRUPPE75QTFAWQ", string privKey = "2AfUSsvr/2o4KLLxa8NtkIHw+EL052i/QwMDIKoT")
        {
            region = r;
            publicKey = pubKey;
            privateKey = privKey;         
        }

        /// <summary>
        /// Creates and returns DynamoDB client object
        /// </summary>
        /// <returns>DynamoDB client</returns>
        public Amazon.DynamoDBv2.AmazonDynamoDBClient getDynamoDBClient()
        {
            try
            {
                var dynamoClient = new Amazon.DynamoDBv2.AmazonDynamoDBClient(this.publicKey, this.privateKey, this.region);
                return dynamoClient;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }          
        }

        /// <summary>
        /// Get DynamoDB client
        /// </summary>
        /// <param name="client"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public Table getTable(Amazon.DynamoDBv2.AmazonDynamoDBClient client, string table)
        {
            Table T = null;
            try
            {
                 T = Table.LoadTable(client, table);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error getting table: {0}", ex.Message);
            }
            return T;
        }
        /// <summary>
        /// Executes put item request
        /// </summary>
        /// <param name="client">dynamodb client</param>
        /// <param name="request">put request</param>
        public void putItem(Amazon.DynamoDBv2.AmazonDynamoDBClient client, PutItemRequest request)
        {
            try
            {
                client.PutItemAsync(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }           
        }

        /// <summary>
        /// executes get item request and returns result
        /// </summary>
        /// <param name="client">dynamodb client</param>
        /// <param name="request">get request</param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<Dictionary<string, AttributeValue> > GetItemAsync(Amazon.DynamoDBv2.AmazonDynamoDBClient client, GetItemRequest request)
        {
            Task<GetItemResponse> taskresponse = client.GetItemAsync(request);

            GetItemResponse response = await taskresponse;

            return response.Item;
        }

        /*public async Task<Dictionary<string, AttributeValue>> LongRunningOperation(Amazon.DynamoDBv2.AmazonDynamoDBClient client, GetItemRequest request)
        {
            System.Console.WriteLine("TRE " + request.ToString());
            try
            {
                var get = client.GetItemAsync(request);
                System.Console.WriteLine("TRE2 " + get.IsCompleted);
                //System.Threading.Thread.Sleep(50000);
                //how to
                return get.Result.Item;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }*/


        public UpdateItemResponse updateItem(Amazon.DynamoDBv2.AmazonDynamoDBClient client, UpdateItemRequest request)
        {

            try
            {
                var update = client.UpdateItemAsync(request);
                return update.Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public QueryResponse getQuery(Amazon.DynamoDBv2.AmazonDynamoDBClient client, QueryRequest request)
        {
            try
            {
                var query = client.QueryAsync(request);
                return query.Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public PutItemRequest makePutRequest(string tablename, Dictionary<string, AttributeValue> item)
        {
            var request = new PutItemRequest
            {
                TableName = tablename,
                Item = item
            };
            return request;
        }

        public GetItemRequest makeGetRequest(string tablename, Dictionary<string, AttributeValue> key)
        {
            var request = new GetItemRequest
            {
                TableName = tablename,
                Key = key
            };
            return request;
        }

        public UpdateItemRequest makeUpdateRequest(string tablename, Dictionary<string, AttributeValue> key, Dictionary<string, string> names, Dictionary<string, AttributeValue> values, string expression)
        {
            var request = new UpdateItemRequest
            {
                TableName = tablename,
                Key = key,
                ExpressionAttributeNames = names,
                ExpressionAttributeValues = values,
                UpdateExpression = expression
            };
            return request;
        }

        public void Dispose()
        {
            publicKey = null;
            privateKey = null;
            region = null;
            GC.Collect();
        }


    }
}