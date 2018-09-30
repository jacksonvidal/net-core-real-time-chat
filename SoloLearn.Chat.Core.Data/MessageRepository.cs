using SoloLearn.Chat.Core.Entities;
using SoloLearn.Chat.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoloLearn.Chat.Core.Data
{
    /// <summary>
    /// All the basic data operations are implemented by abstraction
    /// in here we can create specifics methods to create an custom dataset
    /// </summary>
    public class MessageRepository : RepositoryBase<Message>, IMessageRepository
    {
        public MessageRepository(ChatDbContext context) : base(context) { }
    }
}
