using SoloLearn.Chat.Core.Entities;
using SoloLearn.Chat.Core.Repository;
using SoloLearn.Chat.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoloLearn.Chat.Service
{

    public class RoomService : ServiceBase<Room>, IRoomService
    {
        public RoomService(IRoomRepository repository) : base(repository) { }


    }
}
