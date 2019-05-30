using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQSConsumer
{
    public class Configurations
    {
        // NOTE:
        // The Queue is created using the format:
        // https://{REGION_ENDPOINT}/queue.|api-domain|/{YOUR_ACCOUNT_NUMBER}/{YOUR_QUEUE_NAME}
        public const string QueueURL =
            "https://sqs.us-west-2.amazonaws.com/711453260699/MySQSQueue";

        public const string FIFOQueueURL =
            "https://sqs.us-west-2.amazonaws.com/711453260699/MySQSQueue.fifo";


    }
}
