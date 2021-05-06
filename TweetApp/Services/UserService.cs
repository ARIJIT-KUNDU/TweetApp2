using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetApp.DAL.Interfaces;
using TweetApp.Entities;

namespace TweetApp.Services
{
    public class UserService
    {
        private readonly IMongoCollection<AppUser> _users;
        public UserService(IMongoCollection<AppUser> users)
        {
            
            _users = users;
        }
        
    }
}
