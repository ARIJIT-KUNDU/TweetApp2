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
    [Authorize]
    public class LikesController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILikesRepository _likesRepository;
        private readonly ITweetRepository _tweetRepository;

        public LikesController(IUserRepository userRepository,ILikesRepository likesRepository,ITweetRepository tweetRepository)
        {
            _userRepository = userRepository;
            _likesRepository = likesRepository;
            _tweetRepository = tweetRepository;
        }
        [HttpPost("{tweetId}")]
        
        public void AddLike(int tweetId)
        {
            //try
            //{
            //    var sourceUserId = 2;
            //    var likedTweet = await _tweetRepository.GetTweetByTweetId(tweetId);
            //    var sourceUser = await _userRepository.GetUserByIdAsync(sourceUserId);
            //    if (likedTweet == null) return NotFound();
            //    if (likedTweet.AppUserId == sourceUserId) return BadRequest("You cannot like your own tweet");
            //    var tweetLike = await _likesRepository.GetTweetLike(sourceUserId, likedTweet.Id);
            //    if (tweetLike != null) return BadRequest("You already like this tweet");
            //    tweetLike = new TweetLike
            //    {
            //        SourceUserId = sourceUserId,
            //        LikedTweetId = likedTweet.Id
            //    };
            //    sourceUser.LikedTweets.Add(tweetLike);
            //    //if (await _userRepository.SaveAllAsync(sourceUser)) return Ok();
            //    return BadRequest("Failed to like tweet");
            //}
            //catch(Exception ex)
            //{
            //    return BadRequest("Error occurred while adding like");
            //}
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikeDto>>> GetTweetLikes(string predicate)
        {
            try
            {
                var tweets= await _likesRepository.GetTweetLikes(predicate, 2,2);
                return Ok(tweets);
            }
            catch(Exception ex)
            {
                return BadRequest("Error occurred while getting tweet likes");
            }
        }
    }
}
