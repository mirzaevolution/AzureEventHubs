using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;
namespace EventHubCoreDemo
{
    public class Publisher
    {
        private readonly EventHubClient _eventHubClient;
        public Publisher(string connectionString)
        {
            _eventHubClient = EventHubClient.CreateFromConnectionString(connectionString);
        }
        public async Task PublishAsync<T>(T data)
        {
            string json = JsonConvert.SerializeObject(data);
            byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
            EventData eventData = new EventData(jsonBytes);
            Console.WriteLine("Sending data....");
            await _eventHubClient.SendAsync(eventData);
            Console.WriteLine("Sent successfully.");
        }
        public async Task PublishAsync<T>(IEnumerable<T> listData)
        {
            List<EventData> eventDataList = new List<EventData>();
            foreach (T data in listData)
            {
                string json = JsonConvert.SerializeObject(data);
                byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
                EventData eventData = new EventData(jsonBytes);
                eventDataList.Add(eventData);
            }
            Console.WriteLine("Sending list of data....");
            await _eventHubClient.SendAsync(eventDataList);
            Console.WriteLine("Sent successfully.");

        }
    }
}
