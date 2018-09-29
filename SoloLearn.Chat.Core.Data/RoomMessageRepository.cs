using SoloLearn.Chat.Core.Entities;
using SoloLearn.Chat.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoloLearn.Chat.Core.Data
{
    /// <summary>
    /// Because the EFCore does not have the implementation of Lazy Load and using the 
    /// Generic repository pattern, creating a repo and service for the many-to-many 
    /// entity is necessary.
    /// </summary>
    public class RoomMessageRepository : RepositoryBase<RoomMessages>, IRoomMessageRepository
    {
        public RoomMessageRepository(ChatDbContext context) : base(context) { }
    }
}
