﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetApp.DAL.Interfaces;
using TweetApp.Entities;

namespace TweetApp.DAL.Repositories
{
    public class TweetRepository : ITweetRepository
    {
        private readonly DataContext _context;
        
        public TweetRepository(DataContext context)
        {
            _context = context;
            
        }
        public async Task<IEnumerable<Tweet>> GetTweetsAsync(int memberId)
        {
            try
            {
                return await _context.Tweets.Where(x => x.AppUserId == memberId).ToListAsync();
            }
            catch(Exception ex)
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
