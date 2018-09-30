using SoloLearn.Chat.Core.Entities;
using SoloLearn.Chat.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoloLearn.Chat.Core.Data
{
    /// <summary>
    /// All the basic data operations are implemented by abstraction
    /// in here we can create specifics methods to create an custom dataset
    /// </summary>
    public class RoomRepository : RepositoryBase<Room>, IRoomRepository
    {
        public RoomRepository(ChatDbContext context) : base(context) { }
    }
}
