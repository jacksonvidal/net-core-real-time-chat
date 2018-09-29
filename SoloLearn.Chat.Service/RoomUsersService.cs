using SoloLearn.Chat.Core.Entities;
using SoloLearn.Chat.Core.Repository;
using SoloLearn.Chat.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoloLearn.Chat.Service
{
    public class RoomUsersService : ServiceBase<RoomUsers>, IRoomUsersService
    {
        public RoomUsersService(IRoomUsersRepository repository) : base(repository) { }
    }
}
