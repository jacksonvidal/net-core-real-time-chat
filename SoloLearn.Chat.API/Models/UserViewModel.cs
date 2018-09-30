using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoloLearn.Chat.API.Models
{
    /// <summary>
    /// Simplifyed Model of Users to don't run into a croosed refernce
    /// </summary>
    public class UserViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }

        public string Password { get; set; }

    }
}
