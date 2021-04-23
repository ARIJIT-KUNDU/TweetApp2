using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetApp.Entities;

namespace TweetApp.DAL.Interfaces
{
    public interface ITweetRepository
    {
        public Task<IEnumerable<Tweet>> GetTweetsAsync(int memberId);
        public Task<IEnumerable<Tweet>> GetTweetsAsync();
        public Task<IEnumerable<Tweet>> AddTweet(Tweet tweet);
    }
}
