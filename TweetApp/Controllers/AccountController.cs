using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
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
    public class AccountController : Controller
    {
        private readonly IDataContext _context;
        private IMongoCollection<AppUser> _dbCollection;

        public AccountController(IDataContext context, IOptions<TweetAppDatabaseSettings> options)
        {
            _context = context;
            _dbCollection = _context.tweetappdb.GetCollection<AppUser>(options.Value.UsersCollectionName);

        }
        [HttpPost,Route("register")]
        public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
        {
            try
            {
                if (await UserExists(registerDto.LoginId)) return BadRequest("Login Id is taken");
                if (await UserExists(registerDto.Email)) return BadRequest("Email is taken");

                var user = new AppUser
                {
                    FirstName = registerDto.Firstname,
                    LastName = registerDto.Lastname,
                    
                    Email = registerDto.Email.ToLower(),
                    LoginId=registerDto.LoginId.ToLower(),
                    Password = registerDto.Password,
                    ContactNumber=registerDto.ContactNumber
                };

                await _dbCollection.InsertOneAsync(user);
                return new AppUser
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    
                    Email = user.Email,
                    LoginId=user.LoginId,
                    Password = user.Password,
                    ContactNumber=user.ContactNumber
                };
            }
            catch(Exception ex)
            {
                return BadRequest("Error occurred while registering");
            }
        }
        [HttpPost,Route("login")]
        public async Task<ActionResult<AppUser>> Login(LoginDto loginDto)
        {
            try
            {
                FilterDefinition<AppUser> filter = Builders<AppUser>.Filter.Eq("LoginId", loginDto.Username);
                var user = await _dbCollection.FindAsync(filter).Result.SingleOrDefaultAsync();
                if (user == null) return Unauthorized("Invalid username");
                if (loginDto.Password != user.Password) return Unauthorized("Invalid password");
                return new AppUser
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                   
                    Email = user.Email,
                    LoginId=user.LoginId,
                    Password = user.Password,
                    ContactNumber=user.ContactNumber
                };
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred while logging in");
            }
        }
        private async Task<bool> UserExists(string loginId)
        {
            FilterDefinition<AppUser> filter = Builders<AppUser>.Filter.Eq("LoginId", loginId.ToLower());
            return await _dbCollection.FindAsync(filter).Result.AnyAsync();
        }
    }
}
