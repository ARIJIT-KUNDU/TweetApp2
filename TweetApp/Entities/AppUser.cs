using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetApp.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime Dob { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Tweet> Tweets { get; set; }
    }
}
