using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetApp.DAL.Interfaces;
using TweetApp.Entities;

namespace TweetApp.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        

        public UserRepository(DataContext context)
        {
            _context = context;
            
        }
        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            try
            {
                return await _context.Users.FindAsync(id);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            try
            {
                return await _context.Users.Include(p => p.Tweets).SingleOrDefaultAsync(x => x.Email == username);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            try
            {
                return await _context.Users.Include(p => p.Tweets).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> SaveAllAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(AppUser user)
        {
            try
            {
                _context.Entry(user).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
