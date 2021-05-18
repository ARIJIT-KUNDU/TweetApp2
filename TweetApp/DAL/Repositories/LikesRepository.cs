using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TweetApp.DAL.Interfaces;
using TweetApp.DTOs;
using TweetApp.Entities;

namespace TweetApp.DAL.Repositories
{
    public class LikesRepository : ILikesRepository
    {
        private readonly IMongoCollection<TweetLike> _tweetLikeData;
        public LikesRepository(ITweetAppDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _tweetLikeData = database.GetCollection<TweetLike>("LikeData");
        }

        public bool Create(TweetLike tweetLike)
        {
            bool isCreated = false;
            try
            {
                _tweetLikeData.InsertOne(tweetLike);
                isCreated = true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return isCreated;
        }

        public bool Delete(TweetLike unlike)
        {
            bool isDeleted = false;
            try
            {
                var data = _tweetLikeData.DeleteOne(x=>x.tweetId.Equals(unlike.tweetId) & x.userId.Equals(unlike.userId));
                isDeleted = true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return isDeleted;
        }

        public List<TweetLike> FindAll()
        {
            try
            {
                return _tweetLikeData.Find(LikeModel => true).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<TweetLike> FindAllByCondition(Expression<Func<TweetLike, bool>> expression)
        {
            try
            {
                return _tweetLikeData.Find(expression).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public TweetLike FindByCondition(Expression<Func<TweetLike, bool>> expression)
        {
            try
            {
                return _tweetLikeData.Find(expression).FirstOrDefault();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
