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
        private IMongoCollection<TweetLike> _likesCollection;
        private IMongoCollection<Tweet> _tweetsCollection;
        private IMongoCollection<AppUser> _usersCollection;
        public LikesRepository(IDataContext context, IOptions<TweetAppDatabaseSettings> options)
        {
            _context = context;
            _likesCollection = _context.tweetappdb.GetCollection<TweetLike>(options.Value.LikesCollectionName);
            _tweetsCollection = _context.tweetappdb.GetCollection<Tweet>(options.Value.TweetsCollectionName);
            _usersCollection=_context.tweetappdb.GetCollection<AppUser>(options.Value.UsersCollectionName);
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



                return await _likesCollection.FindAsync(builder.And(conditions)).Result.FirstOrDefaultAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<LikeDto>> GetTweetLikes(string predicate, int userId,int tweetId)
        {
            try
            {
                var tweets = _tweetsCollection.AsQueryable().OrderBy(t => t.Id);
                var likes = _likesCollection.AsQueryable();
                var users = _usersCollection.AsQueryable().OrderBy(t => t.Id);
                if (predicate == "liked")
                {
                    likes = (MongoDB.Driver.Linq.IMongoQueryable<TweetLike>)likes.Where(like => like.SourceUserId == userId);
                    tweets = (IOrderedQueryable<Tweet>)likes.Select(like => like.LikedTweet);
                }
                if (predicate == "likedBy")
                {
                    likes = (MongoDB.Driver.Linq.IMongoQueryable<TweetLike>)likes.Where(like => like.LikedTweetId == tweetId);
                    users = (IOrderedQueryable<AppUser>)likes.Select(like => like.SourceUser);
                }
                //return await _usersCollection.Find(user => new LikeDto
                //{
                //    Id = user.Id,
                //    LoginId = user.LoginId
                //}).toListAsync();
                throw new NotImplementedException();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        

        public async Task<Tweet> GetTweetWithLikes(int userId)
        {
            try
            {

                FilterDefinition<Tweet> filter = Builders<Tweet>.Filter.Eq("AppUserId",userId);
                return await _tweetsCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
                //throw new NotImplementedException();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
