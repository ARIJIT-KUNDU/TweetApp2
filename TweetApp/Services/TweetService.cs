using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetApp.Entities;

namespace TweetApp.Services
{
    public class TweetService
    {
        private readonly IMongoCollection<Tweet> _tweets;

        public TweetService(IMongoCollection<Tweet> tweets)
        {
            _tweets = tweets;
        }
    }
}
