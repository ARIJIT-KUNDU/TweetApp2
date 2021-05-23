using System;
using System.Collections.Generic;
using System.Text;

namespace TweetApp.Streaming.Consumer
{
    class TweetConsumer : ITweetConsumer
    {
        public void Listen(Action message)
        {
            var config = new Dictionary
            {
                {"group.id","booking_consumer" },
                {"bootstrap.servers", "localhost:9092" },
                { "enable.auto.commit", "false" }
            };
            using (var consumer = new Consumer(config, null, new StringDeserializer(Encoding.UTF8)))
            {
                consumer.Subscribe("timemanagement_booking");
                consumer.OnMessage += (_, msg) =>
                {
                    message(msg.Value);
                };

                while (true)
                {
                    consumer.Poll(100);
                }
            }
        }
    }
}
