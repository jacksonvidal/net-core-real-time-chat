using SoloLearn.Chat.Api.Models;
using SoloLearn.Chat.Core.Entities;
using SoloLearn.Chat.Core.Service;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoloLearn.Chat.Api.Hubs
{
    /// <summary>
    /// Chat Hub handles the realtime data
    /// </summary>
    public class ChatHub : Hub
    {
        //Services interfaces
        protected readonly IUserService _userService;
        protected readonly IRoomService _roomService;
        protected readonly IMessageService _messageService;
        protected readonly IRoomUsersService _roomUsersService;

        /// <summary>
        /// constructor to inject the services
        /// </summary>
        /// <param name="userService">User Service</param>
        /// <param name="roomService">Room Service</param>
        /// <param name="messageService">Message Service</param>
        /// <param name="roomUsersService"></param>
        public ChatHub(IUserService userService, IRoomService roomService, IMessageService messageService, IRoomUsersService roomUsersService)
        {
            _userService = userService;
            _roomService = roomService;
            _messageService = messageService;
            _roomUsersService = roomUsersService;
        }

        /// <summary>
        /// Whenever a user connects to the hub, if he's not 
        /// created yet the provider will set it into the groups
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {
            var user = _userService.GetSingleFirst(e => e.UserName == Context.User.Identity.Name);

            if (user == null)
            {
                user = new User { UserName = Context.User.Identity.Name };

                _userService.Add(user);
            }
            else
            {
                foreach (var room in user.Rooms)
                    Groups.AddToGroupAsync(Context.ConnectionId, room.Room.Name);
            }

            return base.OnConnectedAsync();
        }

        /// <summary>
        /// Set's an user into a choosen room
        /// </summary>
        /// <param name="roomName">Name of the room</param>
        /// <returns></returns>
        public async Task JoinRoom(string roomName)
        {
            var room = _roomService.GetSingleFirst(e => e.Name == roomName);
            var user = _userService.GetSingleFirst(e => e.UserName == Context.User.Identity.Name);

            if (room != null && user != null)
            {
                if(_roomService.Count(e => e.Users.Any(r => r.RoomId == room.Id && r.UserId == user.Id)) <= 0)
                {
                    _roomUsersService.Add(new RoomUsers
                    {
                        RoomId = room.Id,
                        UserId = user.Id,
                    });
                }

                await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            }
        }

        /// <summary>
        /// Removes the user from the room
        /// </summary>
        /// <param name="roomName">Name of the room</param>
        /// <returns></returns>
        public async Task LeaveRoom(string roomName)
        {
            var room = _roomService.GetSingleFirst(e => e.Name == roomName);
            var user = _userService.GetSingleFirst(e => e.UserName == Context.User.Identity.Name);

            if (room != null && user != null)
            {
                room.Users.Remove(user.Rooms.FirstOrDefault(e => e.RoomId == room.Id));

                _roomService.Update(room);

                await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
            }
        }

        /// <summary>
        /// Handle the messages transactions between users in a room
        /// </summary>
        /// <param name="message">New Message Model</param>
        /// <returns></returns>
        public async Task Send(NewChatMessageViewModel message)
        {
            var user = _userService.GetSingleFirst(e => e.UserName == Context.User.Identity.Name);
            var room = _roomService.GetSingleFirst(e => e.Name == message.RoomName);

            //Insert the message into the database 
            //to historical porpuses
            var msg = new Message
            {
                Content = message.Content,
                CreatedDate = DateTime.Now,
                UserId = user.Id,
                Rooms = new List<RoomMessages>
                {
                    new RoomMessages { RoomId = room.Id }
                }
            };

            msg.Rooms.FirstOrDefault().Message = msg;

            _messageService.Add(msg);

            //deliver the message to the specific room where the current user is
            await Clients.Group(message.RoomName).SendAsync("Receive", new { message.Content, Author = Context.User.Identity.Name });
        }

    }
}
