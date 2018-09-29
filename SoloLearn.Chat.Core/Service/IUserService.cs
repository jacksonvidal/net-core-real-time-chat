using SoloLearn.Chat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoloLearn.Chat.Core.Service
{
    public interface IUserService : IServiceBase<User>
    {
        User Authenticate(User user);
    }
}
