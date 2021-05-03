using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetApp.Entities;

namespace TweetApp.DAL.Interfaces
{
    public interface IUserRepository
    {
        public void Update(AppUser user);
        //public Task<bool> SaveAllAsync();
        public Task<IEnumerable<AppUser>> GetUsersAsync();
        public Task<AppUser> GetUserByIdAsync(int id);
        public Task<AppUser> GetUserByUsernameAsync(string username);
       
    }
}
