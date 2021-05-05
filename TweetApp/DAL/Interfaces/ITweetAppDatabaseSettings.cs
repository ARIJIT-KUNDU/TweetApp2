using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetApp.DAL.Interfaces
{
    public interface ITweetAppDatabaseSettings
    {
        public string UsersCollectionName { get; set; }
        public string TweetsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
