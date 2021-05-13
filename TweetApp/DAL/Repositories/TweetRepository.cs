using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetApp.DAL.Interfaces;
using TweetApp.Entities;

namespace TweetApp.DAL.Repositories
{
    public class TweetRepository:ITweetRepository
    {
        private readonly IDataContext _context;
        private IMongoCollection<Tweet> _dbCollection;

        public TweetRepository(IDataContext context, IOptions<TweetAppDatabaseSettings> options)
        {
            
            _context = context;
            _dbCollection = _context.tweetappdb.GetCollection<Tweet>(options.Value.TweetsCollectionName);
        }
        public async Task<IEnumerable<Tweet>> GetTweetsAsync(int memberId)
        {
            try
            {

                FilterDefinition<Tweet> filter = Builders<Tweet>.Filter.Eq("AppUserId", memberId);

                return await _dbCollection.FindAsync(filter).Result.ToListAsync();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






        public async Task<Tweet> AddTweet(Tweet tweet)
        {
            try
            {
                FilterDefinition<Tweet> filter = Builders<Tweet>.Filter.Eq("AppUserId", tweet.AppUserId);
                var newTweet = new Tweet
                {
                    Message = tweet.Message,
                    Tag=tweet.Tag,
                    CreatedOn = DateTime.Now,
                    AppUserId = tweet.AppUserId
                };
                await _dbCollection.InsertOneAsync(newTweet);
                return new Tweet
                {
                    Message = newTweet.Message,
                    Tag=newTweet.Tag,
                    CreatedOn = newTweet.CreatedOn,
                    AppUserId = newTweet.AppUserId
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tweet GetTweetByTweetId(int tweetId)
        {
            try
            {
                FilterDefinition<Tweet> filter = Builders<Tweet>.Filter.Eq("Id", tweetId);

                return _dbCollection.Find(filter).FirstOrDefault();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
