using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetApp.DAL.Interfaces;
using TweetApp.Entities;

namespace TweetApp
{
    public class DataContext:IDataContext
    {
        
        private MongoClient _mongoClient { get; set; }

        

        public IMongoDatabase tweetappdb { get; set; }

        public DataContext(IOptions<TweetAppDatabaseSettings> configuration) 
        {
            _mongoClient = new MongoClient(configuration.Value.ConnectionString);
            tweetappdb = _mongoClient.GetDatabase(configuration.Value.DatabaseName);
        }
        

        
    }
}
