using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TweetApp.DAL.Interfaces;
using TweetApp.DTOs;
using TweetApp.Entities;

namespace TweetApp.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version}/[controller]")]
    public class UsersController : Controller
    {
        
        
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public UsersController(IUserRepository userRepository,IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }
        [HttpGet,Route("all")]

        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            try
            {
                var users =await  _userRepository.GetUsersAsync();
                return Ok(users);
            }
            catch(Exception ex)
            {
                return BadRequest("Error occurred while getting list of users");
            }

        }
        [HttpGet, Route("search/{username}")]

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
        [HttpPost, Route("{username}/forgot")]
        public async Task<JsonResult> ResetPassword(PasswordResetDto passwordResetDto, string username)
        {

            try
            {
                var user = await _userRepository.GetUserByUsernameAsync(username);
                if (user.Password == passwordResetDto.OldPassword)
                    user.Password = passwordResetDto.NewPassword;
                _userRepository.Update(user);
                 return new JsonResult("Successfully reset passowrd");
                //return BadRequest("Failed to reset password");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        [HttpGet,Route("getOtherUsers/{loginId}")]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetOtherUsers(string loginId)
        {
            try
            {
                var otherUsers = await _userRepository.GetOtherUsers(loginId);
                return Ok(otherUsers);
            }
            catch(Exception ex)
            {
                return BadRequest("Failed to get other users");
            }
        }

        [HttpGet, Route("getUserById/{userId}")]

        public async Task<ActionResult<AppUser>> GetUserById(string userId)
        {
            try
            {
                return await _userRepository.GetUserByIdAsync(userId);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred while getting user details by username");
            }

        }

        
    }
}
