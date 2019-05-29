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

        public SQSMessageSender()
        {
            sqsClient = SQSClientProvider.GetSQSClient();
        }
        public bool SendMessage(string message)
        {
            SendMessageRequest sendMsgReq = new SendMessageRequest();
            sendMsgReq.QueueUrl = Configurations.QueueURL;
            sendMsgReq.MessageBody = message;

            SendMessageResponse sendMessageResponse =
                sqsClient.SendMessage(sendMsgReq);

            if (sendMessageResponse.HttpStatusCode == HttpStatusCode.OK)
                return true;

            return false;
        }


    }
}
