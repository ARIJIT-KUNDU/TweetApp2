using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetApp.DTOs;
using TweetApp.Entities;

namespace TweetApp.DAL.Interfaces
{
    public interface ILikesRepository
    {
        Task<TweetLike> GetTweetLike(int sourceUserId, int likedTweetId);
        Task<Tweet> GetTweetWithLikes(int tweetId);
        Task<IEnumerable<LikeDto>> GetTweetLikes(string predicate, int userId,int tweetId);
    }
}
