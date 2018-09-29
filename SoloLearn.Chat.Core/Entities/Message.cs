using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SoloLearn.Chat.Core.Entities
{
    public class Message : EntityBase
    {
        public Message()
        {
            Rooms = new List<RoomMessages>();
        }

        [Required]
        public string Content { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public ICollection<RoomMessages> Rooms { get; set; }
    }

    public class RoomMessages
    {
        [Key]
        public int RoomId { get; set; }

        [Key]
        public int MessageId { get; set; }

        public Message Message { get; set; }
        public Room Room { get; set; }
    }
}
