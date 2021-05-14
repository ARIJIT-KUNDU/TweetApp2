using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetApp.Entities;

namespace TweetApp.DAL.Interfaces
{
    public interface ITweetRepository
    {
        public Task<IEnumerable<Tweet>> GetTweetsAsync(string memberId);
        public Task<IEnumerable<Tweet>> GetAllTweetsAsync();
        public Task<Tweet> AddTweet(Tweet tweet);
        public Tweet GetTweetByTweetId(string tweetId);
        public Task<IEnumerable<Tweet>> GetTweetsByUsernameAsync(string username);
        public bool Update(Tweet tweet);
        public bool Delete(string tweetId);
    }
}
