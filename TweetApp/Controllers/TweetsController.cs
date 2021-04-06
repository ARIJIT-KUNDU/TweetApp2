using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetApp.DAL.Interfaces;
using TweetApp.Entities;

namespace TweetApp.Controllers
{
    public class TweetsController : Controller
    {
        private readonly ITweetRepository _tweetRepository;
        

        public TweetsController(ITweetRepository tweetRepository)
        {
            _tweetRepository = tweetRepository;
            
        }
        [HttpGet,Route("tweets/{memberId}")]

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
    }
}
