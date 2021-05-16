using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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
        private readonly ILikesRepository _likesRepository;

        public TweetsController(ITweetRepository tweetRepository,ITweetCommentsRepository tweetCommentsRepository,ILikesRepository likesRepository)
        {
            _tweetRepository = tweetRepository;
            _tweetCommentsRepository = tweetCommentsRepository;
            _likesRepository = likesRepository;
        }
        
        [HttpGet,Route("getTweets/{memberId}")]

        public async Task<ActionResult<IEnumerable<Tweet>>> GetTweets(string memberId)
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

        [Route("tweetById/{tweetId}/userId/{userId}")]
        [HttpGet]
        public Tweet GetTweetById(string tweetId, string userId)
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
                tweetCommentsModels = null;

                List<TweetLike> tweetLikesModel = new List<TweetLike>();

                tweetLikesModel = GetTweetLikesByTweetId(tweetId);
                if (tweetLikesModel.Count != 0)
                {
                    if (tweetLikesModel[0].tweetId == tweetId)
                    {
                        tweet.likesCount = tweetLikesModel.Count;
                    }
                }
                tweetLikesModel = null;

                var tweetLike = _likesRepository.FindByCondition(x=>x.tweetId.Equals(tweetId) & x.userId.Equals(userId));
                if (tweetLike != null)
                {
                    tweet.likeId = tweetLike.likeId;
                }

            }
            catch (Exception ex)
            {
                string message = "Meesage : " + ex.Message + " & Stacktrace: " + ex.StackTrace;
            }

            return tweet;
        }

        [Route("tweetCommentsById/{tweetId}")]
        [HttpGet]
        public List<TweetComments> GetTweetCommentsById(string tweetId)
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

        [HttpGet,Route("all")]
        public async Task<ActionResult<Tweet>> GetAllTweetsAsync()
        {
            try
            {
                var tweets= await _tweetRepository.GetAllTweetsAsync();
                return Ok(tweets);
            }
            catch(Exception ex)
            {
                return BadRequest("Error occurred while getting all tweets");
            }
        }

        [HttpGet,Route("{username}")]
        public async Task<ActionResult<Tweet>> GetTweetsByUsername(string username)
        {
            List<TweetLike> allLikes = new List<TweetLike>();
            List<Tweet> allTweets = new List<Tweet>();
            try
            {
                var tweets = await _tweetRepository.GetTweetsByUsernameAsync(username);
                //get all likes for the DB
                //allLikes=await _l
                return Ok(tweets);
            }
            catch(Exception ex)
            {
                return BadRequest("Error occurred while getting tweets by username");
            }
        }

        [HttpPut,Route("{username}/update/{id}")]
        public JsonResult UpdateTweet([FromBody] Tweet tweetModel,string id)
        {
            bool status = false;
            string response;
            try
            {
                tweetModel.CreatedOn = DateTime.Now;
                tweetModel.TweetId = id;
                status = _tweetRepository.Update(tweetModel);
                if (status)
                {
                    response = "Tweet updated";
                }
                else
                {
                    response = "Unable to update";
                }
            }
            catch(Exception ex)
            {
                response = "error";
                throw ex;
            }
            return new JsonResult(response);
        }
        [HttpDelete,Route("{username}/delete/{id}")]
        public JsonResult DeleteTweet(string id)
        {
            bool status = false;
            try
            {
                status = _tweetRepository.Delete(id);
                if (status)
                {
                    return new JsonResult("Tweet deleted");
                }
                else
                {
                    return new JsonResult("Unable to delete tweet");
                }
            }
            catch(Exception ex)
            {
                string message = "Message: " + ex.Message + " & Stacktrace: " + ex.StackTrace;
            }
            return new JsonResult("Error");
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
