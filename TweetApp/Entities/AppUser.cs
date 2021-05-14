using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TweetApp.Entities
{
    [DataContract]
    [BsonIgnoreExtraElements]
    public class AppUser
    {
        
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string LoginId { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        
        [JsonIgnore]
        public ICollection<Tweet> Tweets { get; set; }
        [JsonIgnore]
        public ICollection<TweetLike> LikedTweets { get; set; }
    }
}
