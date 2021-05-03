using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetApp.Entities;

namespace TweetApp.DAL.Interfaces
{
    public interface IDataContext
    {
        IMongoDatabase tweetappdb { get; }
    }
}
