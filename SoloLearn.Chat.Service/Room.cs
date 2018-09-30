using SoloLearn.Chat.Core.Entities;
using SoloLearn.Chat.Core.Repository;
using SoloLearn.Chat.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoloLearn.Chat.Service
{
    /// <summary>
    /// All the basic data operations are implemented by abstraction, now the developer can focus on business rules
    /// </summary>
    public class RoomService : ServiceBase<Room>, IRoomService
    {
        public RoomService(IRoomRepository repository) : base(repository) { }
    }
}
