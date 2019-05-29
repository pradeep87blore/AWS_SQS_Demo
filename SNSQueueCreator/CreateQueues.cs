using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace SQSQueueCreator
{
    public class CreateQueues
    {
        private static AmazonSQSClient sqsClient = null;
        public static bool CreateSQSQueue(string queueName)
        {
            try
            {
                if (sqsClient == null)
                    InitializeSQSClient();

                CreateQueueRequest createQueueRequest =
                    new CreateQueueRequest();

                createQueueRequest.QueueName = queueName;

                CreateQueueResponse createQueueResponse =
                    sqsClient.CreateQueue(createQueueRequest);

                if (createQueueResponse.HttpStatusCode == HttpStatusCode.OK)
                    return true;
            }
            catch (AmazonSQSException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
            }

            return false;
        }

        static bool InitializeSQSClient()
        {
            try
            {
                sqsClient = SQSClientProvider.GetSQSClient();

                return true;
            }
            catch (AmazonSQSException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return false;
        }


        //private const string PROFILE_NAME = "SQSDeveloper";
        //private static bool GetCredentials(out AWSCredentials awsCredentials)
        //{
        //    var chain = new CredentialProfileStoreChain();
        //    if (chain.TryGetAWSCredentials(PROFILE_NAME, out awsCredentials))
        //    {
        //        return true;
        //    }

        //    return false; // No matching profile found
        //}

    }
}
