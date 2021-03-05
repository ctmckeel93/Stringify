using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Stringify.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        public int UserId { get; set; }

        public int MessageId { get; set; }

        public User Commenter { get; set; }

        public Message Message { get; set; }

        public string MyComment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;






    }
}