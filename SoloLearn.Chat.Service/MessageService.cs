using SoloLearn.Chat.Core.Entities;
using SoloLearn.Chat.Core.Repository;
using SoloLearn.Chat.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoloLearn.Chat.Service
{   
    /// <summary>
    /// All the basic data operations are implemented by abstraction, now the developer can focus on business rules
    /// </summary>
    public class MessageService : ServiceBase<Message>, IMessageService
    {
        public MessageService(IMessageRepository repository) : base(repository) { }

        public override void Add(Message entity)
        {
            

            base.Add(entity);
        }
    }
}
