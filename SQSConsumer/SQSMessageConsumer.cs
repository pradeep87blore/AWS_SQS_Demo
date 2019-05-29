using Amazon.SQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.SQS;
using SQSQueueCreator;

namespace SQSConsumer
{
    public class SQSMessageConsumer
    {
        public delegate void ReceivedMessageDel(string message);

        private static event ReceivedMessageDel ReceivedMessageEvent = null;

        private static AmazonSQSClient sqsClient = null;

        public SQSMessageConsumer(ReceivedMessageDel subscriber)
        {
            ReceivedMessageEvent += subscriber;

            sqsClient = SQSClientProvider.GetSQSClient();

            //StartListeningToMessages();
        }

        public void FetchMessages()
        {
            ReceiveMessageRequest receiveMessageRequest = new ReceiveMessageRequest();
            
            receiveMessageRequest.QueueUrl = Configurations.QueueURL;

            ReceiveMessageResponse receiveMessageResponse =
                sqsClient.ReceiveMessage(receiveMessageRequest);

            if (receiveMessageResponse != null)
            {
                foreach (var msg in receiveMessageResponse.Messages)
                {
                    ReceivedMessageEvent(msg.Body);
                }
            }
        }
    }
}
