using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TweetApp.Entities
{
    public class MyKafkaConsumer: BackgroundService
    {
        private readonly ConsumerConfig _consumerConfig;
        private readonly IOptions<TweetAppDatabaseSettings> _appSettings;

        
        public MyKafkaConsumer(ConsumerConfig consumerConfig, IOptions<TweetAppDatabaseSettings> appSettings)
        {
            _consumerConfig = consumerConfig;


            _appSettings = appSettings;

        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Run(() => StartConsumer(stoppingToken));
            return Task.CompletedTask;
        }
        private async Task StartConsumer(CancellationToken stoppingToken)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };
            using(var c=new ConsumerBuilder<Ignore, string>(_consumerConfig).Build())
            {
                //c.Subscribe(_topicName);
                while (!stoppingToken.IsCancellationRequested)
                {
                    var cr = c.Consume(cts.Token);
                    Console.WriteLine($"Consumed messages '{cr.Message.Value}' at '{cr.TopicPartitionOffset}'");
                }
            }
            throw new NotImplementedException();
        }
    }
}
