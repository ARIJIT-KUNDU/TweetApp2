using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TweetApp.Entities;

namespace TweetApp.DAL.Interfaces
{
    public interface ITweetCommentsRepository
    {
        List<TweetComments> FindAll();
        List<TweetComments> FindAllByCondition(string tweetId);
        TweetComments FindByCondition(Expression<Func<TweetComments, bool>> expression);
        bool Create(TweetComments tweetComment);
        bool Update(TweetComments tweetComment);
        bool Delete(ObjectId tweetCommentId);
    }
}
