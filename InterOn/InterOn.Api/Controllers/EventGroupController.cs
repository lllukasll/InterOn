using System.Threading.Tasks;
using InterOn.Data.ModelsDto.Event;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterOn.Api.Controllers
{   [Authorize]
    [Route("api/group/{groupId}/event")]
    public class EventGroupController : Controller
    {
        private readonly IEventService _service;

        public EventGroupController(IEventService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> CreateEventForGroup(int groupId,[FromBody] CreateEventDto eventDto)
        {
            if (await _service.ExistGroup(groupId) == false)
                return BadRequest("Nie ma grupy o tym Id");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userId = int.Parse(HttpContext.User.Identity.Name);
            await _service.CreateEventForGroupAsync(eventDto, groupId,userId);
            return Ok();
        }
        [HttpPut("{eventId}")]
        public async Task<IActionResult> UpdateEventGroup(int eventId,int groupId, [FromBody] UpdateEventDto eventDto)
        {
            var userId = int.Parse(HttpContext.User.Identity.Name);
            if (await _service.ExistGroup(groupId) == false)
                return BadRequest("Nie ma grupy o tym Id");
            if (await _service.IsAdminEvent(eventId,userId) == false)
                return BadRequest("Nie jesteś adminem eventu");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
           
            await _service.UpdateEventAsync(eventId,eventDto);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEventGroup( int groupId)
        {
            if (await _service.ExistGroup(groupId) == false)
                return BadRequest("Nie ma grupy o tym Id");
            var result = await _service.GetAllEventGroupAsync(groupId);
            if (result == null)
                return NotFound("Nie ma żadnego wydarzenia dla tej grupy");
            return Ok(result);
        }
        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetEventGroup(int eventId,int groupId)
        {
            if (await _service.ExistGroup(groupId) == false)
                return BadRequest("Nie ma grupy o tym Id");
            var result = await _service.GetEventAsync(eventId,groupId);
            if (result == null)
                return NotFound("Nie ma żadnego wydarzenia dla tej grupy");
            return Ok(result);
        }
        [HttpDelete("{eventId}")]
        public async Task<IActionResult> DeleteEventGroup(int eventId, int groupId)
        {
            var userId = int.Parse(HttpContext.User.Identity.Name);
            if (await _service.ExistGroup(groupId) == false)
                return BadRequest("Nie ma grupy o tym Id");
            if (await _service.IsAdminEvent(eventId, userId) == false)
                return BadRequest("Nie jesteś adminem eventu");

            await _service.Delete(eventId);
            return Ok();
        }
    }
}