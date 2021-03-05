using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stringify.Models
{
    public class User
    {
        [Key]

        public int UserId { get; set; }


        [Required]
        [MinLength(2, ErrorMessage = "First name must have at least 2 characters")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Last name must have at least 2 characters")]
        public string LastName { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Username must be between 3 and 15 characters")]
        [MaxLength(15, ErrorMessage = "Username must be between 3 and 15 characters")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must have at least 8 characters")]
        public string Password { get; set; }


        [NotMapped]
        [Compare("Password", ErrorMessage = "Passwords must match!")]
        // [DataType(DataType.Password)]
        public string Confirm { get; set; }

        public List<Message> AllPosts { get; set; }
        public List<Comment> UserComments { get; set; }

        // [InverseProperty("SendingUser")]
        // public List<Message> SentMessages {get;set;}

        // [InverseProperty("RecievingUser")]
        // public List<Message> RecievedMessages {get;set;}

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;


    }
}