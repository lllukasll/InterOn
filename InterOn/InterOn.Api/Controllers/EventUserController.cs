using System.Threading.Tasks;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterOn.Api.Controllers
{
    [Authorize]
    [Route("/api/event/{eventId}/user")]
    public class EventUserController : Controller
    {
        private readonly IEventService _service;

        public EventUserController(IEventService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUserEvent(int eventId)
        {
            if (await _service.ExistEvent(eventId) == false)
            {
                return NotFound();
            }
            var userId = int.Parse(HttpContext.User.Identity.Name);
            if (await _service.IfUserBelongToEvent(eventId, userId))
                return BadRequest("Użytkownik należy już do Wydarzenia");

            await _service.CreateEventUserAsync(eventId, userId);
            return Ok();
        }
       
        [HttpDelete]
        public async Task<IActionResult> RemoveUserEvent(int eventId)
        {
            var userId = int.Parse(HttpContext.User.Identity.Name);
            if (await _service.IfUserBelongToEvent(eventId, userId) == false)
                return BadRequest("Użytkownik nie należy do grupy");
            await _service.RemoveUserEvent(userId, eventId);

            return Ok();
        }
    }
}