using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TweetApp.Entities
{
    public class Tweet
    {
        
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string TweetId { get; set; }

        [Required]
        public string Message { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        public string Tag { get; set; }
        public List<TweetComments> Replies { get; set; }
        [Required]
        
        public string AppUserId { get; set; }
        [JsonIgnore]
        public ICollection<TweetLike> LikedByUSer { get; set; }
        public int commentsCount { get; set; }
    }
}
