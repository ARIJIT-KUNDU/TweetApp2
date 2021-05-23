using System;
using System.Collections.Generic;
using System.Text;

namespace TweetApp.Streaming.Consumer
{
    public interface ITweetConsumer
    {
        void Listen(Action message);
    }
}
