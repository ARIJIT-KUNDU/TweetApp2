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
    [Route("api/[controller]")]
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
                if (await UserExists(registerDto.Email)) return BadRequest("Username is taken");

                var user = new AppUser
                {
                    FirstName = registerDto.Firstname,
                    LastName = registerDto.Lastname,
                    Gender = registerDto.Gender,
                    Dob = registerDto.Dob,
                    Email = registerDto.Email.ToLower(),
                    Password = registerDto.Password
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return new AppUser
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    Dob = user.Dob,
                    Email = user.Email,
                    Password = user.Password
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
                var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == loginDto.Username);
                if (user == null) return Unauthorized("Invalid username");
                if (loginDto.Password != user.Password) return Unauthorized("Invalid password");
                return new AppUser
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    Dob = user.Dob,
                    Email = user.Email,
                    Password = user.Password
                };
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred while logging in");
            }
        }
        private async Task<bool> UserExists(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email.ToLower());
        }
    }
}
