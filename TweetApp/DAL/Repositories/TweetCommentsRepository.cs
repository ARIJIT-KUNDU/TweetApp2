using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TweetApp.DAL.Interfaces;
using TweetApp.Entities;

namespace TweetApp.DAL.Repositories
{
    public class TweetCommentsRepository : ITweetCommentsRepository
    {
        private readonly IMongoCollection<TweetComments> _tweetCommentsData;

        public TweetCommentsRepository(ITweetAppDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _tweetCommentsData = database.GetCollection<TweetComments>("ReplyData");
        }

        public List<TweetComments> FindAll()
        {
            try
            {
                return _tweetCommentsData.Find(CommentModel => true).ToList();
            } 
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<TweetComments> FindAllByCondition(string tweetId)
        {
            try
            {
                return _tweetCommentsData.Find(x => x.tweetId.Equals(tweetId)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TweetComments FindByCondition(Expression<Func<TweetComments, bool>> expression)
        {
            try
            {
                return _tweetCommentsData.Find(expression).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Create(TweetComments tweetComment)
        {
            bool isCreated = false;
            try
            {
                _tweetCommentsData.InsertOne(tweetComment);
                isCreated = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isCreated;
        }

        public bool Update(TweetComments tweetComment)
        {
            bool isUpdated = false;
            try
            {
                _tweetCommentsData.ReplaceOne(x => x.CommentId.Equals(tweetComment.CommentId), tweetComment);
                isUpdated = true;
            }
            catch (Exception)
            {
                throw;
            }
            return isUpdated;
        }

        public bool Delete(ObjectId tweetCommentId)
        {
            bool isDeleted = false;
            try
            {
                _tweetCommentsData.DeleteOne(x => x.CommentId.Equals(tweetCommentId));
                isDeleted = true;
            }
            catch (Exception)
            {
                throw;
            }
            return isDeleted;
        }
    }
}



