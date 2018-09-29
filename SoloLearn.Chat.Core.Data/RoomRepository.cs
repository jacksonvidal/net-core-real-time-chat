using SoloLearn.Chat.Core.Entities;
using SoloLearn.Chat.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoloLearn.Chat.Core.Data
{
    public class RoomRepository : RepositoryBase<Room>, IRoomRepository
    {
        public RoomRepository(ChatDbContext context) : base(context) { }
    }
}
