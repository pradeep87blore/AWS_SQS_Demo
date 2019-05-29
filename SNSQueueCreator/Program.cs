using System;

namespace SQSQueueCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            CreateQueues.CreateSQSQueue("MySQSQueue");
        }
    }
}
