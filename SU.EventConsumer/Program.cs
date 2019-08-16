using Newtonsoft.Json;
using Polly;
using SU.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;

namespace SU.EventConsumer
{
    class Program
    {
        private static HttpClient client = new HttpClient();
        private static Timer Timer = new Timer(TimeSpan.FromSeconds(10).TotalMilliseconds);
        private static int CurrentStep = 0;
        private static AsyncPolicy RetryPolicy { get; set; }


        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            RetryPolicy = Policy.Handle<Exception>()
                .WaitAndRetryAsync(30, attempt => TimeSpan.FromMilliseconds(250) * Math.Pow(2, attempt), (ex, _) =>
                {
                    Console.WriteLine(ex.Message);
                    Timer.Stop();
                });
            
            client = new HttpClient();

            Timer.Elapsed += async (_, __) => await RetryPolicy.ExecuteAsync(() => GetEvents());
            Timer.Start();

            Console.ReadKey();
        }

        private async static Task GetEvents()
        {
            var batchSize = 100;
            var jsonEvents = await client.GetStringAsync($"https://localhost:44359/api/Event?start={CurrentStep}&count={batchSize}");

            var events = JsonConvert.DeserializeObject<IEnumerable<BasketModifiedEvent>>(jsonEvents);
            foreach (var ev in events)
            {
                Console.WriteLine($"{ev.Timestamp}: {ev.Message}");
            }
            if (events.Any())
            {
                CurrentStep = events.Max(x => x.Id);
            }
            Timer.Start();
        }
    }
}
