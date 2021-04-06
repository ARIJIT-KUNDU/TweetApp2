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
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        

        public UsersController(IUserRepository userRepository)
        {

            _userRepository = userRepository;
            
        }
        [HttpGet,Route("users")]

        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetUsersAsync();
                return Ok(users);
            }
            catch(Exception ex)
            {
                return BadRequest("Error occurred while getting list of users");
            }

        }
        [HttpGet,Route("users/{username}")]

        public async Task<ActionResult<AppUser>> GetUser(string username)
        {
            try
            {
                return await _userRepository.GetUserByUsernameAsync(username);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred while getting user details by username");
            }

        }
        [HttpPut,Route("resetPassword")]
        public async Task<ActionResult> ResetPassword(PasswordResetDto passwordResetDto,string username)
        {

            try
            {
                var user = await _userRepository.GetUserByUsernameAsync(username);
                if (user.Password == passwordResetDto.OldPassword)
                    user.Password = passwordResetDto.NewPassword;
                _userRepository.Update(user);
                if (await _userRepository.SaveAllAsync()) return NoContent();
                return BadRequest("Failed to reset password");
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred while resetting password");
            }
        }
        //[HttpPost("add-tweet")]
        //public async Task<TweetDto> AddTweet(TweetDto tweet)
        //{
        //    var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
        //    var result = await _tweetService.AddTweetAsync(tweet);
        //}
    }
}
