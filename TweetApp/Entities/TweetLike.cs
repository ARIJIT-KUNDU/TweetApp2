using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetApp.Entities
{
    public class TweetLike
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string likeId { get; set; }

        public string liked { get; set; }

        
        public string tweetId { get; set; }

        
        public string userId { get; set; }
        public string username { get; set; }
        

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime createdAt { get; set; }
    }
}
