using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stringify.Models
{
    public class Message
    {
        [Key]

        public int MessageId { get; set; }

        public int UserId { get; set; }

        public User Poster { get; set; }

        public string Post { get; set; }

        public List<Comment> AllComments { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;



        // public int SendingUserId { get; set; }
        // public virtual User SendingUser {get;set;}

        // public int RecievingUserId {get;set;}
        // public virtual User RecievingUser {get;set;}

        // public int ConversationId {get;set;}
        //  public virtual Conversation Convo {get;set;}
        // public List<Conversation> AllUsers { get; set; }
    }
}