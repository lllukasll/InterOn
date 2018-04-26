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
            var userId = int.Parse(HttpContext.User.Identity.Name);
            if (!await _service.IsAdminEvent(id, userId))
                return BadRequest("Użytkownik nie jest administratorem i nie może edytować eventu");
            if (await _service.ExistEvent(id) == false)
            {
                return NotFound("Nie ma takiego eventu");
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _service.UpdateEventAsync(id, eventDto);

            return Ok();                
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvent()
        {
            var result = await _service.GetAllEventAsync();
            if (result == null)
                return NotFound("Nie ma żadnego wydarzenia");
            return Ok(result);
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetEvent(int eventId)
        {
            var result = await _service.GetEventAsync(eventId);
            if (result == null)
                return NotFound("Nie ma takiego wydarzenia publicznego");
            return Ok(result);
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> DeleteEvent(int eventId)
        {
            var userId = int.Parse(HttpContext.User.Identity.Name);
            if (await _service.IsAdminEvent(eventId, userId) == false)
                return BadRequest("Nie jesteś adminem eventu");

            await _service.Delete(eventId);
            return Ok();
        }
    }
}