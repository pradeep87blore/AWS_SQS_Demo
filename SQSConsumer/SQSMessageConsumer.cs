﻿using Amazon.SQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            receiveMessageRequest.MaxNumberOfMessages = 10;
            receiveMessageRequest.WaitTimeSeconds = 1;
            ReceiveMessageResponse receiveMessageResponse =
                sqsClient.ReceiveMessage(receiveMessageRequest);

            // This queue doesn't guarantee the order of the messages.
            if (receiveMessageResponse != null)
            {
                foreach (var msg in receiveMessageResponse.Messages)
                {
                    ReceivedMessageEvent(msg.Body);

                    DeleteMessageFromQueue(msg.ReceiptHandle);
                }
            }
        }

        private bool DeleteMessageFromQueue(string msgReceiptHandle)
        {
            DeleteMessageRequest deleteMessageRequest = new DeleteMessageRequest();

            deleteMessageRequest.QueueUrl = Configurations.QueueURL;
            deleteMessageRequest.ReceiptHandle = msgReceiptHandle;

            DeleteMessageResponse response =
                sqsClient.DeleteMessage(deleteMessageRequest);

            if (response.HttpStatusCode == HttpStatusCode.OK)
                return true;

            return false;
        }
    }
}
