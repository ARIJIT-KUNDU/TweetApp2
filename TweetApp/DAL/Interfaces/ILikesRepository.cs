using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TweetApp.DTOs;
using TweetApp.Entities;

namespace TweetApp.DAL.Interfaces
{
    public interface ILikesRepository
    {
        List<TweetLike> FindAll();
        List<TweetLike> FindAllByCondition(Expression<Func<TweetLike, bool>> expression);
        TweetLike FindByCondition(Expression<Func<TweetLike, bool>> expression);
        bool Create(TweetLike tweetLike);
        bool Delete(TweetLike unlike);
    }
}
