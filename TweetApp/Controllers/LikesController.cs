using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetApp.DAL.Interfaces;
using TweetApp.DTOs;
using TweetApp.Entities;

namespace TweetApp.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version}/[controller]")]
    
    public class LikesController : Controller
    {
        private readonly ILikesRepository _likesRepository;

        public LikesController(ILikesRepository likesRepository)
        {
            _likesRepository = likesRepository;
        }
        [HttpPost,Route("{username}/like/{id}")]
        public JsonResult TweetLikeUnlikeAction([FromBody] TweetLike tweetLikesModel)
        {
            try
            {
                if (tweetLikesModel.liked == "like")
                {
                    tweetLikesModel.createdAt = DateTime.Now;
                    var likeStatus = _likesRepository.Create(tweetLikesModel);
                    if (likeStatus)
                    {
                        return new JsonResult("Tweet liked successfully");
                    }
                }
                else
                {
                    var unlikeStatus = _likesRepository.Delete(tweetLikesModel);
                    if (unlikeStatus)
                    {
                        return new JsonResult("Tweet unliked successfully");
                    }
                }


            }
            catch (Exception)
            {
                throw;
            }
            return new JsonResult("Tweet not liked successfully");
        }

        [HttpGet]
        [Route("GetTweetLikesByTweetId/{tweetId}")]
        public List<TweetLike> GetTweetLikesByTweetId(string tweetId)
        {
            List<TweetLike> tweetLikedModel = new List<TweetLike>();
            try
            {
                tweetLikedModel = _likesRepository.FindAllByCondition(x=>x.tweetId.Equals(tweetId));
            }
            catch (Exception ex)
            {
                string message = "Meesage : " + ex.Message + " & Stacktrace: " + ex.StackTrace;
            }
            return tweetLikedModel;
        }
    }
}
