using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TweetApp.Entities
{
    public class TweetComments
    {
        
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string CommentId { get; set; }
        [Required(ErrorMessage ="Comment is required")]
        public string comment { get; set; }
        
        public string tweetId { get; set; }
        
        public string userId { get; set; }
        public string username { get; set; }
        [BsonDateTimeOptions(Kind =DateTimeKind.Local)]
        public DateTime createdAt { get; set; }
    }
}
