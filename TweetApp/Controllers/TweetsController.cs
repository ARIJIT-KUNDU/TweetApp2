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
        

        public TweetsController(ITweetRepository tweetRepository)
        {
            _tweetRepository = tweetRepository;
            
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
        public async Task<ActionResult<Tweet>> AddTweet(string message,int id) 
        {
            try
            {
                var newTweet = await _tweetRepository.AddTweet(message, id);
                return Ok(newTweet);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred while adding tweet");
            }
        }

    }
}
