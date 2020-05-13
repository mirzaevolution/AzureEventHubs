using System;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;
using Microsoft.Azure.EventHubs;

using EventHubCoreDemo;
using Microsoft.Azure.EventHubs.Processor;

namespace Demo001.Receiver
{
    class Program
    {
        static void Receive()
        {
            try
            {
                string hostName = Guid.NewGuid().ToString();

                //single partition
                //string eventHubPath = "demo001";

                //multi partition
                string eventHubPath = "demo001multipartition";

                string consumerGroup = PartitionReceiver.DefaultConsumerGroupName;

                //single partition
                //string eventHubConnectionString = "Endpoint=sb://mgr-evo.servicebus.windows.net/;SharedAccessKeyName=Subscriber;SharedAccessKey=Gh0ovVawNx5OeAOsY6iErfcNKvjPOhJgq2aDV1E8f+c=;EntityPath=demo001";

                //multi partition
                string eventHubConnectionString = "Endpoint=sb://mgr-evo.servicebus.windows.net/;SharedAccessKeyName=Consumer;SharedAccessKey=pKdh2Gihj1VeUxuEziOvi5F768HF+IZ7oXit/aXpVh8=;EntityPath=demo001multipartition";


                string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=ave21corestorage;AccountKey=4DtEkomn9JV1BjMODFmtm9VRnzuAkhVejm3rEXhMCWVBq5Z8KaNkU0ZbC4DsgKx4fxeZ1SKm/a1jN/FGCC95gg==;EndpointSuffix=core.windows.net;";
                string storageContainerName = "eventhubtest";
                EventProcessorHost eventProcessorHost = new EventProcessorHost(
                        hostName: hostName,
                        eventHubPath: eventHubPath,
                        consumerGroupName: consumerGroup,
                        eventHubConnectionString: eventHubConnectionString,
                        storageConnectionString: storageConnectionString,
                        storageContainerName
                    );
                EventProcessorOptions options = new EventProcessorOptions();
                eventProcessorHost.RegisterEventProcessorAsync<Consumer>(options);

                Console.WriteLine("Press ENTER to quit");
                Console.ReadLine();
                eventProcessorHost.UnregisterEventProcessorAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        static void Main(string[] args)
        {
            Receive();
        }
    }
}
