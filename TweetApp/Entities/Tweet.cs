using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetApp.Entities
{
    public class Tweet
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public int Id { get; set; }
        
        public string Message { get; set; }
        public DateTime CreatedOn { get; set; }

        public int AppUserId { get; set; }
    }
}
