using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetApp.DAL.Interfaces;
using TweetApp.DTOs;
using TweetApp.Entities;

namespace TweetApp.DAL.Repositories
{
    public class LikesRepository : ILikesRepository
    {
        private readonly IDataContext _context;
        private IMongoCollection<TweetLike> _dbCollection;
        public LikesRepository(IDataContext context, IOptions<TweetAppDatabaseSettings> options)
        {
            _context = context;
            _dbCollection = _context.tweetappdb.GetCollection<TweetLike>(options.Value.LikesCollectionName);
        }

        
        public async Task<TweetLike> GetTweetLike(int sourceUserId, int likedTweetId)
        {
            try
            {
                var builder = Builders<TweetLike>.Filter;
                List<FilterDefinition<TweetLike>> conditions = new List<FilterDefinition<TweetLike>>();

                if (sourceUserId != 0)
                    conditions.Add(builder.Where(x => x.SourceUserId == sourceUserId));

                if (likedTweetId != 0)
                    conditions.Add(builder.Where(x => x.LikedTweetId == likedTweetId));



                return await _dbCollection.FindAsync(builder.And(conditions)).Result.FirstOrDefaultAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Task<IEnumerable<LikeDto>> GetTweetLikes(string predicate, int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Tweet> GetTweetWithLikes(int tweetId)
        {
            try
            {

                //FilterDefinition<TweetLike> filter = Builders<TweetLike>.Filter.Exists(x=>x.LikedTweet);
                //return await _dbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
                throw new NotImplementedException();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
