using System;
using System.Collections.Generic;
using System.Text;
using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.SQS;

namespace SQSQueueCreator
{
    internal class SQSClientProvider
    {
        static AmazonSQSClient client;

        // https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/net-dg-config-creds.html#sdk-store
        private const string PROFILE_NAME = "S3AccessProfile";
        private static bool GetCredentials(out AWSCredentials awsCredentials)
        {
            var chain = new CredentialProfileStoreChain("C:\\Users\\AWSUser\\AWS\\Credentials");
            if (chain.TryGetAWSCredentials(PROFILE_NAME, out awsCredentials))
            {
                return true;
            }

            return false; // No matching profile found
        }

        public static AmazonSQSClient GetSQSClient()
        {
            if (client == null)
            {
                AWSCredentials awsCredentials = null;

                if (GetCredentials(out awsCredentials))
                {
                    client = new AmazonSQSClient(awsCredentials, RegionEndpoint.USWest2);
                }
            }

            return client;
        }
    }
}
