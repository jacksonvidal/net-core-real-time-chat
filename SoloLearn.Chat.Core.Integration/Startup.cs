using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SoloLearn.Chat.Core.Data;
using SoloLearn.Chat.Core.Repository;
using SoloLearn.Chat.Core.Service;
using SoloLearn.Chat.Service;

namespace SoloLearn.Chat.Core.Integration
{
    /// <summary>
    /// Creates the injection and the database connection, 
    /// using in a separeted project provides the isolation
    /// from the client application with the repository and service layers
    /// </summary>
    public static class Startup
    {
        public static IServiceCollection Run(IServiceCollection services, IConfiguration configuration)
        {
            //create the database connection
            services.AddDbContext<ChatDbContext>(options => options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            //injects the Repsoitory with the interfaces
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoomRepository, RoomRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoomMessageRepository, RoomMessageRepository>();
            services.AddTransient<IRoomUsersRepository, RoomUsersRepository>();

            //injects the Service with the interfaces
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
