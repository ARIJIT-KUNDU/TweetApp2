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
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        
        
        
        public int UserId { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Email { get; set; }
        
        public string LoginId { get; set; }
        
        public string Password { get; set; }
        
        public long ContactNumber { get; set; }
        [JsonIgnore]
        public ICollection<Tweet> Tweets { get; set; }
    }
}
