using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoloLearn.Chat.Api.Models
{
    public class NewChatMessageViewModel
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public string RoomName { get; set; }
        
        public string Author { get; set; }

    }
}
