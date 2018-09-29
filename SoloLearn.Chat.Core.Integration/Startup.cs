using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoloLearn.Chat.Core.Data;
using SoloLearn.Chat.Core.Entities;
using SoloLearn.Chat.Core.Repository;
using SoloLearn.Chat.Core.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SoloLearn.Chat.Service;

namespace SoloLearn.Chat.Core.Integration
{
    public static class Startup
    {
        public static IServiceCollection Run(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ChatDbContext>(options => options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoomRepository, RoomRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoomMessageRepository, RoomMessageRepository>();
            services.AddTransient<IRoomUsersRepository, RoomUsersRepository>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoomService, RoomService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoomMessageService, RoomMessageService>();
            services.AddTransient<IRoomUsersService, RoomUsersService>();



            return services;
        }
    }
}
