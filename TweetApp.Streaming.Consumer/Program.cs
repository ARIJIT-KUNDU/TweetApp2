using System;

namespace TweetApp.Streaming.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var tweetConsumer = new TweetConsumer();
            tweetConsumer.Listen(Console.WriteLine);
        }
    }
}
