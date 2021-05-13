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
            return _tweetCommentsData.Find(CommentModel => true).ToList(); ;
        }

        public List<TweetComments> FindAllByCondition(int tweetId)
        {
            return _tweetCommentsData.Find(x => x.tweetId.Equals(tweetId)).ToList();
        }

        public TweetComments FindByCondition(Expression<Func<TweetComments, bool>> expression)
        {
            return _tweetCommentsData.Find(expression).FirstOrDefault();
        }

        public bool Create(TweetComments tweetComment)
        {
            bool isCreated = false;
            try
            {
                _tweetCommentsData.InsertOne(tweetComment);
                isCreated = true;
            }
            catch (Exception)
            {
                throw;
            }
            return isCreated;
        }

        public bool Update(TweetComments tweetComment)
        {
            bool isUpdated = false;
            try
            {
                _tweetCommentsData.ReplaceOne(x => x.commentId.Equals(tweetComment.commentId), tweetComment);
                isUpdated = true;
            }
            catch (Exception)
            {
                throw;
            }
            return isUpdated;
        }

        public bool Delete(string tweetCommentId)
        {
            bool isDeleted = false;
            try
            {
                _tweetCommentsData.DeleteOne(x => x.commentId.Equals(tweetCommentId));
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



