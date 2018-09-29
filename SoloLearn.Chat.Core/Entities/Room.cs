using System;
using System.Collections.Generic;
using System.Text;

namespace SoloLearn.Chat.Core.Entities
{
    public class Room : EntityBase
    {
        public Room()
        {
            Messages = new List<RoomMessages>();
            Users = new List<RoomUsers>();
        }

        public string Name { get; set; }

        public ICollection<RoomMessages> Messages { get; set; }
        public ICollection<RoomUsers> Users { get; set; }
    }
}
