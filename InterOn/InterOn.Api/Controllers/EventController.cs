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

            var result = await _service.UpdateEventAsync(id, eventDto);

            return Ok(result);                
        }
     
    }
}