using SoloLearn.Chat.Core.Entities;
using SoloLearn.Chat.Core.Repository;
using SoloLearn.Chat.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoloLearn.Chat.Service
{
    /// <summary>
    /// Because the lazy load it's not implemented yet on EF Core and I chose to use the abstract 
    /// repository and service pattern it is necessary to create this structure to handle the many-to-many relashionships
    /// </summary>
    public class RoomUsersService : ServiceBase<RoomUsers>, IRoomUsersService
    {
        public RoomUsersService(IRoomUsersRepository repository) : base(repository) { }
    }
}
