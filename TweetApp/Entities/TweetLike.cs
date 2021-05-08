using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetApp.Entities
{
    public class TweetLike
    {
        public AppUser SourceUser { get; set; }
        public int SourceUserId { get; set; }
        public Tweet LikedTweet { get; set; }
        public int LikedTweetId { get; set; }
    }
}
