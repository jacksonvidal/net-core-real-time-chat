using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoloLearn.Chat.Api.Models;
using SoloLearn.Chat.Core.Service;

namespace SoloLearn.Chat.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Message")]
    public class MessageController : Controller
    {
        protected readonly IMessageService _messageService;
        protected readonly IRoomService _roomService;
        protected readonly IRoomMessageService _roomMessageService;
        protected readonly IUserService _userService;

        public MessageController(IMessageService messageService, IRoomService roomService, IRoomMessageService roomMessageService, IUserService userService)
        {
            this._messageService = messageService;
            this._roomService = roomService;
            this._roomMessageService = roomMessageService;
            this._userService = userService;
        }

        /// <summary>
        /// Retrive the last 5 messages from a room
        /// </summary>
        /// <param name="roomName">;Name of the room</param>
        /// <returns>List od ChatMessages</returns>
        [HttpGet("History")]
        public List<ChatMessageViewModel> History(string roomName)
        {
            var lastMessages = new List<ChatMessageViewModel>();

            var roomMessages = _roomMessageService.GetAll(e => e.Room.Name == roomName).TakeLast(5).ToList();

            foreach (var message in roomMessages)
            {
                var msg = _messageService.GetSingleFirst(e => e.Id == message.MessageId);

                msg.User = _userService.GetSingleFirst(e => e.Id == msg.UserId);

                lastMessages.Add(new ChatMessageViewModel(msg));
            }

            return lastMessages;
        }

    }
}