using SoloLearn.Chat.Core.Repository;
using SoloLearn.Chat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoloLearn.Chat.Core.Data
{
    /// <summary>
    /// All the basic data operations are implemented by abstraction
    /// in here we can create specifics methods to create an custom dataset
    /// </summary>
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ChatDbContext context) : base(context) { }
    }
}
