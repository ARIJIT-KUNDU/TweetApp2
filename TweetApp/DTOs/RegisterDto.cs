using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TweetApp.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        
        [Required]
        public string Email { get; set; }
        [Required]
        public string LoginId { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public long ContactNumber { get; set; }
    }
}
