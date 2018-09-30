using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoloLearn.Chat.Api.Models;
using SoloLearn.Chat.Core.Entities;
using SoloLearn.Chat.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SoloLearn.Chat.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Rooms")]
    [Authorize]
    public class RoomsController : Controller
    {
        //Intefaces of the services
        protected readonly IRoomService _roomService;
        protected readonly IMessageService _messageService;

        /// <summary>
        /// injection of the services
        /// </summary>
        /// <param name="roomService">Room Service</param>
        /// <param name="messageService">Message Service</param>
        public RoomsController(IRoomService roomService, IMessageService messageService)
        {
            _roomService = roomService;
            _messageService = messageService;
        }

        /// <summary>
        /// List all avaliable rooms
        /// </summary>
        /// <returns>List of Rooms</returns>
        [HttpGet]
        public IList<RoomViewModel> Get()
        {
            var rooms = _roomService.GetAll().Select(room => new RoomViewModel
            {
                Id = room.Id,
                Name = room.Name

            }).ToList();

            return rooms;
        }

        /// <summary>
        /// Retrives a single room
        /// </summary>
        /// <param name="id">Room Id</param>
        /// <returns>Room Model</returns>
        [HttpGet("{id}")]
        public RoomViewModel Get(int id)
        {
            var room = _roomService.GetSingleFirst(e => e.Id == id);

            return new RoomViewModel { Id = room.Id, Name = room.Name };
        }

        /// <summary>
        /// Create a new room
        /// </summary>
        /// <param name="room">Room ViewModel</param>
        /// <returns>JSON message</returns>
        [HttpPost]
        public JsonResult Post([FromBody]RoomViewModel room)
        {
            try
            {
                _roomService.Add(new Room { Name = room.Name });

                return Json(new { message = "room created" });
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Remove an specific Room
        /// </summary>
        /// <param name="id">Room Id</param>
        /// <returns>JSON message</returns>
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            try
            {
                var room = _roomService.GetSingleFirst(e => e.Id == id);

                if (room != null)
                    _roomService.Remove(room);

                return Json(new { message = "room removed" });

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message });
            }

        }
    }
}