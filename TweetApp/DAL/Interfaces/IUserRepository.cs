using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TweetApp.Entities;

namespace TweetApp.DAL.Interfaces
{
    public interface IUserRepository
    {
        public void Update(AppUser user);
        public Task<ReplaceOneResult> SaveAllAsync(AppUser user);
        public Task<IEnumerable<AppUser>> GetUsersAsync();
        public Task<AppUser> GetUserByIdAsync(string id);
        public Task<AppUser> GetUserByUsernameAsync(string username);
        public Task<IEnumerable<AppUser>> GetOtherUsers(string loginId);
        
    }
}
