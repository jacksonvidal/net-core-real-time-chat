using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SoloLearn.Chat.Core.Entities
{
    public class User : EntityBase
    {
        public User()
        {
            Rooms = new List<RoomUsers>();
        }
        
        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public List<RoomUsers> Rooms { get; set; }
    }

    public class RoomUsers
    {
        [Key]
        public int UserId { get; set; }
        
        [Key]
        public int RoomId { get; set; }

        public User User { get; set; }
        public Room Room { get; set; }
    }
}
