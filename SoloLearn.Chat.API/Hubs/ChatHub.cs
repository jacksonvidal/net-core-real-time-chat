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
    public class ChatHub : Hub
    {
        protected readonly IUserService _userService;
        protected readonly IRoomService _roomService;
        protected readonly IMessageService _messageService;
        protected readonly IRoomUsersService _roomUsersService;

        public ChatHub(IUserService userService, IRoomService roomService, IMessageService messageService, IRoomUsersService roomUsersService)
        {
            _userService = userService;
            _roomService = roomService;
            _messageService = messageService;
            _roomUsersService = roomUsersService;
        }

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

        public async Task Send(NewChatMessageViewModel message)
        {
            var user = _userService.GetSingleFirst(e => e.UserName == Context.User.Identity.Name);
            var room = _roomService.GetSingleFirst(e => e.Name == message.RoomName);

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

            await Clients.Group(message.RoomName).SendAsync("Receive", new { message.Content, Author = Context.User.Identity.Name });
        }

    }
}
