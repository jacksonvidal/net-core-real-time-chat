using SoloLearn.Chat.Core.Entities;
using SoloLearn.Chat.Core.Repository;
using SoloLearn.Chat.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoloLearn.Chat.Service
{
    public class RoomMessageService : ServiceBase<RoomMessages>, IRoomMessageService
    {
        public RoomMessageService(IRoomMessageRepository repository) : base(repository) { }
    }
}
