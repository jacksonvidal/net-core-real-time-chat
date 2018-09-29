using SoloLearn.Chat.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoloLearn.Chat.Api.Models
{
    public class ChatMessageViewModel
    {
        public ChatMessageViewModel() { }

        public ChatMessageViewModel(Message message)
        {
            Content = message.Content;
            Author = message.User.UserName;
            Timestamp = message.CreatedDate;
        }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Timestamp { get; set; }
    }
}
