using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetApp.DAL.Interfaces;

namespace TweetApp.Entities
{
    public class TweetAppDatabaseSettings : ITweetAppDatabaseSettings
    {
        public string UsersCollectionName { get ; set ; }
        public string TweetsCollectionName { get; set; }
        public string LikesCollectionName { get; set; }
        public string ConnectionString { get ; set ; }
        public string DatabaseName { get ; set ; }
    }
}
