using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using SQSQueueCreator;

namespace SQSConsumer
{
    public class SQSMessageSender
    {
        private static AmazonSQSClient sqsClient = null;

        private static bool useFIFOQueue = false;

        public SQSMessageSender()
        {
            sqsClient = SQSClientProvider.GetSQSClient();
        }

        public void UseFIFOQueue(bool bEnableFIFOQueueUse)
        {
            useFIFOQueue = bEnableFIFOQueueUse;
        }

        public bool SendMessage(string message)
        {
            try
            {
                SendMessageRequest sendMsgReq = new SendMessageRequest();
                if (useFIFOQueue == true)
                {
                    sendMsgReq.QueueUrl = Configurations.FIFOQueueURL;
                    sendMsgReq.MessageGroupId = "MyFIFOQueueMsgGroup"; // This is a mandatory param for FIFO queues.
                    // It is used to group messages in the FIFO order

                    sendMsgReq.MessageDeduplicationId = Guid.NewGuid().ToString();
                }
                else
                {
                    sendMsgReq.QueueUrl = Configurations.QueueURL;
                }
                sendMsgReq.MessageBody = message;
                

                SendMessageResponse sendMessageResponse =
                    sqsClient.SendMessage(sendMsgReq);

                if (sendMessageResponse.HttpStatusCode == HttpStatusCode.OK)
                    return true;
            }
            catch (AmazonSQSException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return false;
        }


    }
}
