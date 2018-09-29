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
        protected readonly IRoomService _roomService;
        protected readonly IMessageService _messageService;

        public RoomsController(IRoomService roomService, IMessageService messageService)
        {
            _roomService = roomService;
            _messageService = messageService;
        }

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

        [HttpGet("{id}")]
        public RoomViewModel Get(int id)
        {
            var room = _roomService.GetSingleFirst(e => e.Id == id);

            return new RoomViewModel { Id = room.Id, Name = room.Name };
        }
        
        [HttpPost]
        public ActionResult Post([FromBody]RoomViewModel room)
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