using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoloLearn.Chat.Api.Models
{
    /// <summary>
    /// Simplifyed object to make the hadnle faster for the Hub
    /// </summary>
    public class NewChatMessageViewModel
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public string RoomName { get; set; }
        
        public string Author { get; set; }

    }
}
