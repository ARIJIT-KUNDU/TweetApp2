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
    public class TweetRepository 
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
        //public void Update(AppUser user)
        //{
        //    try
        //    {
        //        _context.Entry(user).State = EntityState.Modified;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task<IEnumerable<Tweet>> GetTweetsAsync()
        //{
        //    try
        //    {
        //        return await _context.Tweets.ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public Task<IEnumerable<Tweet>> AddTweet(Tweet tweet)
        //{
        //    try
        //    {

        //    }
        //    catch { }
        //}
    }
}
