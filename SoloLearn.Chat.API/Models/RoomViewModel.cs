using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoloLearn.Chat.Api.Models
{
    /// <summary>
    /// Creates the ViewModel for the Room
    /// </summary>
    public class RoomViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public List<ChatMessageViewModel> LastMessages { get; set; }
    }
}
