using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
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
        private IMongoCollection<Tweet> _tweetDbCollection;
        private IMongoCollection<AppUser> _userDbCollection;

        public TweetRepository(IDataContext context, IOptions<TweetAppDatabaseSettings> options)
        {
            
            _context = context;
            _tweetDbCollection = _context.tweetappdb.GetCollection<Tweet>(options.Value.TweetsCollectionName);
            _userDbCollection = _context.tweetappdb.GetCollection<AppUser>(options.Value.UsersCollectionName);
        }
        public async Task<IEnumerable<Tweet>> GetTweetsAsync(string memberId)
        {
            try
            {

                FilterDefinition<Tweet> filter = Builders<Tweet>.Filter.Eq("AppUserId", memberId);

                return await _tweetDbCollection.FindAsync(filter).Result.ToListAsync();


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
                    TweetId=tweet.TweetId,
                    Message = tweet.Message,
                    Tag=tweet.Tag,
                    CreatedOn = DateTime.Now,
                    AppUserId = tweet.AppUserId
                };
                await _tweetDbCollection.InsertOneAsync(newTweet);
                return new Tweet
                {
                    TweetId=newTweet.TweetId,
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

        public Tweet GetTweetByTweetId(string tweetId)
        {
            try
            {
                FilterDefinition<Tweet> filter = Builders<Tweet>.Filter.Eq("TweetId", tweetId);

                return _tweetDbCollection.Find(filter).FirstOrDefault();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Tweet>> GetAllTweetsAsync()
        {
            try
            {
                return await _tweetDbCollection.FindAsync(Tweet => true).Result.ToListAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Tweet>> GetTweetsByUsernameAsync(string username)
        {
            try
            {
                AppUser user = new AppUser();
                user = _userDbCollection.Find(x => x.LoginId.Equals(username)).FirstOrDefault();
                if (user != null)
                {
                    return await _tweetDbCollection.FindAsync(x => x.AppUserId.Equals(user.UserId)).Result.ToListAsync();
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(Tweet tweet)
        {
            bool isUpdated = false;
            try
            {
                var existingTweet=_tweetDbCollection.Find(x => x.TweetId.Equals(tweet.TweetId)).FirstOrDefault();
                Tweet newTweet = new Tweet
                {
                    TweetId=existingTweet.TweetId,
                    Message = tweet.Message,
                    Tag = tweet.Tag,
                    AppUserId=tweet.AppUserId,
                    CreatedOn = DateTime.Now
                };
                _tweetDbCollection.ReplaceOne(x => x.TweetId.Equals(existingTweet.TweetId), newTweet);
                isUpdated = true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return isUpdated;
        }

        public bool Delete(string tweetId)
        {
            bool isDeleted = false;
            try
            {
                _tweetDbCollection.DeleteOne(x => x.TweetId.Equals(tweetId));
                isDeleted = true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return isDeleted;
        }
    }
}
