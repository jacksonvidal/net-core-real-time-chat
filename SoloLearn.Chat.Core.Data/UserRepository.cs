using SoloLearn.Chat.Core.Repository;
using SoloLearn.Chat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoloLearn.Chat.Core.Data
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ChatDbContext context) : base(context) { }
    }
}
