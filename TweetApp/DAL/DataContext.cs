using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetApp.Entities;

namespace TweetApp
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<AppUser> Users { get; set; }
        public virtual DbSet<Tweet> Tweets { get; set; }
    }
}
