using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetApp.DTOs;
using TweetApp.Entities;

namespace TweetApp.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AccountController : Controller
    {
        private readonly DataContext _context;
        

        public AccountController(DataContext context)
        {
            _context = context;
            
        }
        [HttpPost,Route("register")]
        public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
        {
            try
            {
                if (await UserExists(registerDto.LoginId)) return BadRequest("Username is taken");

                var user = new AppUser
                {
                    FirstName = registerDto.Firstname,
                    LastName = registerDto.Lastname,
                    
                    Email = registerDto.Email.ToLower(),
                    LoginId=registerDto.LoginId.ToLower(),
                    Password = registerDto.Password,
                    ContactNumber=registerDto.ContactNumber
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
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
        [HttpGet,Route("login")]
        public async Task<ActionResult<AppUser>> Login(LoginDto loginDto)
        {
            try
            {
                var user = await _context.Users.SingleOrDefaultAsync(x => x.LoginId == loginDto.Username);
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
            return await _context.Users.AnyAsync(x => x.LoginId == loginId.ToLower());
        }
    }
}
