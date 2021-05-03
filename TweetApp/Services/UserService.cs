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
        public UserService(ITweetAppDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<AppUser>(settings.UsersCollectionName);
        }
        public List<AppUser> Get()
        {
            List<AppUser> users;
            users = _users.Find(user => true).ToList();
            return users;
        }
    }
}
