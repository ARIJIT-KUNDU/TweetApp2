using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public string Message { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        public string Tag { get; set; }
        public List<string> Replies { get; set; }
        [Required]
        public int AppUserId { get; set; }
    }
}
