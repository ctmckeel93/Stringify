using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stringify.Models
{
    public class Conversation
    {
        [Key]

        public int ConversationId { get; set; }

        public int SenderId { get; set; }
        public User Sender { get; set; }

        public int RecieverId { get; set; }
        public User Reciever { get; set; }



    }
}