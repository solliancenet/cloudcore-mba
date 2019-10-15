using Contoso.Apps.SportsLeague.Data.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace Contoso.Apps.SportsLeague.Web.Helpers
{
    public static class AzureQueueHelper
    {
        private static readonly CloudStorageAccount storageAccount;
        private static readonly CloudQueueClient queueClient;
        private static readonly CloudQueue receiptQueue;
        private static readonly CloudQueue smsQueue;

        static AzureQueueHelper()
        {
            // Retrieve the storage account from a connection string in the web.config file.
            storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.AppSettings["AzureQueueConnectionString"]);

            // Create the cloud queue client.
            queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to our queue.
            receiptQueue = queueClient.GetQueueReference("receiptgenerator");
            smsQueue = queueClient.GetQueueReference("sms-notifications");
        }

        /// <summary>
        /// Create a message in our Azure Queue, which will be sent to our Worker Role in order
        /// to generate a Pdf file that gets saved to blob storage, and can be emailed to the client.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static async Task QueueReceiptRequest(Order order)
        {
            // Create the queue if it doesn't already exist.
            if (await receiptQueue.CreateIfNotExistsAsync())
            {
                Console.WriteLine("Queue '{0}' Created", receiptQueue.Name);
            }
            else
            {
                Console.WriteLine("Queue '{0}' Exists", receiptQueue.Name);
            }

            var jsonOrder = JsonConvert.SerializeObject(order);
            // Create a message and add it to the queue.
            CloudQueueMessage message = new CloudQueueMessage(jsonOrder);
            
            // Async enqueue the message.
            await receiptQueue.AddMessageAsync(message);
        }

        public static async Task QueueSmsMessageRequest(Order order)
        {
            // Only send order to the queue if the user opted into receiving an SMS message.
            if (!order.SMSOptIn)
            {
                return;
            }

            if (await smsQueue.CreateIfNotExistsAsync())
            {
                Console.WriteLine("Queue '{0}' Created", smsQueue.Name);
            }
            else
            {
                Console.WriteLine("Queue '{0}' Exists", smsQueue.Name);
            }

            var jsonOrder = JsonConvert.SerializeObject(order);
            // Create a message and add it to the queue.
            var message = new CloudQueueMessage(jsonOrder);

            // Async enqueue the message.
            await smsQueue.AddMessageAsync(message);
        }
    }
}