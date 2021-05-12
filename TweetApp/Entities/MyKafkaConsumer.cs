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
            while (!stoppingToken.IsCancellationRequested)
            { //read user from kafka  
                using (var consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build())
                {
                    consumer.Subscribe("kafkaListenTopic_From_Producer");
                    var consumeResult = consumer.Consume().Message.Value;
                    //if (!consumeResult.IsNullOrEmpty())
                    //{ //write your business logic to invoke IMyBusinessServices here  
                    //}
                }
            }
        }
    }
}
