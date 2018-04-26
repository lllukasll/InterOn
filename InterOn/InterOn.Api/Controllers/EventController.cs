using System;
using InterOn.Data.ModelsDto.Event;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InterOn.Api.Controllers
{
    [Authorize]
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
            var userId = int.Parse(HttpContext.User.Identity.Name);
            await _service.CreateEventAsync(userId,eventDto);
            
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] UpdateEventDto eventDto)
        {
            try
            {
                var userId = int.Parse(HttpContext.User.Identity.Name);
                if (!await _service.IsAdminEvent(id, userId))
                    return BadRequest("Użytkownik nie jest administratorem i nie może edytować eventu");
                if (await _service.ExistEvent(id) == false)
                {
                    return NotFound("Nie ma takiego eventu");
                }
                if (!ModelState.IsValid) return BadRequest(ModelState);



                var result = await _service.UpdateEventAsync(id, eventDto);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException);
            }
          
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