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
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TweetsController : Controller
    {
        private readonly ITweetRepository _tweetRepository;
        private readonly DataContext _context;

        public TweetsController(ITweetRepository tweetRepository,DataContext context)
        {
            _tweetRepository = tweetRepository;
            _context = context;
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
        [HttpGet, Route("all")]
        public async Task<ActionResult<IEnumerable<Tweet>>> GetTweets()
        {
            try
            {
                var tweets = await _tweetRepository.GetTweetsAsync();
                return Ok(tweets);
            }
            catch(Exception ex)
            {
                return BadRequest("Error occurred while getting tweets");
            }
        }
        [HttpPost, Route("{memberId}/add")]
        public async Task<Tweet> AddTweet(Tweet tweet)
        {

        }

    }
}
