using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
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
        private readonly IDataContext _context;
        private IMongoCollection<AppUser> _dbCollection;

        public UserRepository(IDataContext context,IOptions<TweetAppDatabaseSettings> options)
        {
            _context = context;
            _dbCollection = _context.tweetappdb.GetCollection<AppUser>(options.Value.UsersCollectionName);
        }
        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            try
            {
                
                FilterDefinition<AppUser> filter = Builders<AppUser>.Filter.Eq("Id", id);

                return await _dbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
                

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
                FilterDefinition<AppUser> filter = Builders<AppUser>.Filter.Eq("LoginId", username);
                return await _dbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
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
                var users = await _dbCollection.Find(_=>true).ToListAsync(); 
                return users;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public async Task<bool> SaveAllAsync()
        //{
        //    try
        //    {
        //        return await _dbCollection.SaveChangesAsync() > 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void Update(AppUser user)
        {
            try
            {
                _dbCollection.ReplaceOne(m => m._id == user._id, user);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
