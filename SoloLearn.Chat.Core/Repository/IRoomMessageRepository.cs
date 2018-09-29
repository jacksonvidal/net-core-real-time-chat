using SoloLearn.Chat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoloLearn.Chat.Core.Repository
{
    /// <summary>
    /// Because the EFCore does not have the implementation of Lazy Load and using the 
    /// Generic repository pattern, creating a repo and service for the many-to-many 
    /// entity is necessary.
    /// </summary>
    public interface IRoomMessageRepository : IRepositoryBase<RoomMessages>
    {
    }
}
