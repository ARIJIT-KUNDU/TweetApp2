﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetApp.Entities;

namespace TweetApp.DAL.Interfaces
{
    public interface ITweetRepository
    {
        public Task<IEnumerable<Tweet>> GetTweetsAsync(int memberId);
        
        public Task<Tweet> AddTweet(string message,int id);
        public Task<Tweet> GetTweetByTweetId(int tweetId);
    }
}
