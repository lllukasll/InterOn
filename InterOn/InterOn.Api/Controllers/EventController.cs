using InterOn.Data.ModelsDto.Event;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InterOn.Api.Controllers
{
    [Route("api/event")]
    public class EventController : Controller
    {
        private readonly IEventService _service;

        public EventController(IEventService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventDto eventDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _service.CreateEventAsync(eventDto);
            
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] UpdateEventDto eventDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (await _service.ExistEvent(id) == false)
            {
                return NotFound();
            }

            var result = await _service.UpdateEventAsync(id, eventDto);

            return Ok(result);
        }
        [Authorize]
        [HttpPost("user/{eventId}")]
        public async Task<IActionResult> CreateUserEvent(int eventId)
        {
            if (await _service.ExistEvent(eventId) == false)
            {
                return NotFound();
            }
            var userId = int.Parse(HttpContext.User.Identity.Name);
            if (await _service.IfUserBelongToEvent(eventId, userId))
                return BadRequest("Użytkownik należy już do Wydarzenia");

            var result = await _service.CreateEventUserAsync(eventId, userId);
            return Ok(result);
        }
        [Authorize]
        [HttpDelete("user/{eventId}")]
        public async Task<IActionResult> RemoveUserEvent(int eventId)
        {
            var userId = int.Parse(HttpContext.User.Identity.Name);
            if (await _service.IfUserBelongToEvent(eventId, userId)==false)
                return BadRequest("Użytkownik nie należy do grupy");
            await _service.RemoveUserEvent(userId, eventId);

            return Ok();
        }
    }
}