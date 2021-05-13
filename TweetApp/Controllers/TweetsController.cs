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
    public class TweetsController : Controller
    {
        private readonly ITweetRepository _tweetRepository;
        private readonly ITweetCommentsRepository _tweetCommentsRepository;

        public TweetsController(ITweetRepository tweetRepository,ITweetCommentsRepository tweetCommentsRepository)
        {
            _tweetRepository = tweetRepository;
            _tweetCommentsRepository = tweetCommentsRepository;
        }
        
        [HttpGet,Route("{memberId}")]

        public async Task<ActionResult<IEnumerable<Tweet>>> GetTweets(int memberId)
        {
            try
            {
                var tweets = await _tweetRepository.GetTweetsAsync(memberId);
                return Ok(tweets);
            }
            catch(Exception ex)
            {
                return BadRequest("Error occurred while getting tweets");
            }


        }

        [HttpPost, Route("{memberId}/add")]
        public async Task<ActionResult<Tweet>> AddTweet(Tweet tweet) 
        {
            try
            {
                var newTweet = await _tweetRepository.AddTweet(tweet);
                return newTweet;
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred while adding tweet");
            }
        }

        [Route("{username}/reply/{id}")]
        [HttpPost]
        public JsonResult ReplyTweet([FromBody] TweetComments addComment)
        {
            try
            {
                addComment.createdAt = DateTime.Now;
                bool creationStatus = _tweetCommentsRepository.Create(addComment);
                if (creationStatus)
                {
                    return new JsonResult("Tweet replied successfully");
                }
            }
            catch (Exception ex)
            {
                string message = "Meesage : " + ex.Message + " & Stacktrace: " + ex.StackTrace;
            }
            return new JsonResult("Error");
        }

        [Route("tweetById/{tweetId}/userId/{userID}")]
        [HttpGet]
        public Tweet GetTweetById(int tweetId, string userId)
        {
            Tweet tweet = new Tweet();

            try
            {
                tweet = _tweetRepository.GetTweetByTweetId(tweetId);

                List<TweetComments> tweetCommentsModels = new List<TweetComments>();
                tweetCommentsModels = GetTweetCommentsById(tweetId);
                if (tweetCommentsModels.Count != 0)
                {
                    if (tweetCommentsModels[0].tweetId == tweetId)
                    {
                        tweet.commentsCount = tweetCommentsModels.Count;
                    }
                }
                //tweetCommentsModels = null;

                //List<TweetLikesModel> tweetLikesModel = new List<TweetLikesModel>();

                //tweetLikesModel = GetTweetLikesByTweetId(tweetId);
                //if (tweetLikesModel.Count != 0)
                //{
                //    if (tweetLikesModel[0].tweetId == tweetId)
                //    {
                //        tweet.likesCount = tweetLikesModel.Count;
                //    }
                //}
                //tweetLikesModel = null;

                //var tweetLike = _tweetService.GetLikeByTweetIdandUserID(tweetId, userId);
                //if (tweetLike != null)
                //{
                //    tweet.likeId = tweetLike.likeId;
                //}

            }
            catch (Exception ex)
            {
                string message = "Meesage : " + ex.Message + " & Stacktrace: " + ex.StackTrace;
            }

            return tweet;
        }

        [Route("tweetCommentsById/{tweetId}")]
        [HttpGet]
        public List<TweetComments> GetTweetCommentsById(int tweetId)
        {
            List<TweetComments> tweetCommentsModels = new List<TweetComments>();
            try
            {
                
                tweetCommentsModels = _tweetCommentsRepository.FindAllByCondition(tweetId);
                
            }
            catch (Exception ex)
            {
                string message = "Meesage : " + ex.Message + " & Stacktrace: " + ex.StackTrace;
            }

            return tweetCommentsModels;
        }

    }
}
