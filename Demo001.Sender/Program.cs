using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Azure.EventHubs;
using EventHubCoreDemo;

namespace Demo001.Sender
{
    public class MessagePayload
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Message { get; set; }
        public string DateTimeJson { get; set; } = DateTime.Now.ToString("o");
    }
    class Program
    {
        static void Main(string[] args)
        {
            //single partition
            //Publisher publisher = new Publisher("Endpoint=sb://mgr-evo.servicebus.windows.net/;SharedAccessKeyName=Publisher;SharedAccessKey=NTK6/GmP9ke+5GsEptAPtOM/HWwIlqSFLk12cRJY5+0=;EntityPath=demo001");

            //multi partition
            Publisher publisher = new Publisher("Endpoint=sb://mgr-evo.servicebus.windows.net/;SharedAccessKeyName=Publisher;SharedAccessKey=5kERRYjQQCcq2Ntx2VbPX/oKB5BmpdQqEcU5pf6QDQA=;EntityPath=demo001multipartition");

            string input = string.Empty;
            do
            {
                Console.Write("Enter message to send ('exit' to quit the app): ");
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input.Trim()))
                {
                    publisher.PublishAsync<MessagePayload>(new MessagePayload
                    {
                        Message = input
                    }).Wait();

                }
                else
                {
                    Console.WriteLine("Please input non-empty message!");
                }
            }
            while (!input.ToLower().Trim().Equals("exit"));
        }
    }
}
