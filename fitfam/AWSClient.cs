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
            var dynamoClient = new Amazon.DynamoDBv2.AmazonDynamoDBClient(this.publicKey, this.privateKey, this.region);
            return dynamoClient;
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