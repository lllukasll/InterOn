using System.IO;
using System.Threading.Tasks;
using InterOn.Api.Helpers;
using InterOn.Data.ModelsDto.Event;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace InterOn.Api.Controllers
{   [Authorize]
    [Route("api/group/{groupId}/event")]
    public class EventGroupController : Controller
    {
        private readonly IHostingEnvironment _host;
        private readonly IEventService _service;
        private readonly PhotoSettings _photoSettings;

        public EventGroupController(IOptions<PhotoSettings> options, IHostingEnvironment host, IEventService service)
        {
            _photoSettings = options.Value;
            _host = host;
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
        [HttpPost]
        [Route("{eventId}/photo")]
        public async Task<IActionResult> Upload(int groupId,int eventId, IFormFile file)
        {
            if (await _service.ExistEvent(eventId) == false) return NotFound();
            var userId = int.Parse(HttpContext.User.Identity.Name);
            if (await _service.ExistGroup(groupId) == false)
                return BadRequest("Nie ma grupy o tym Id");
            if (await _service.IsAdminEvent(eventId, userId) == false)
                return BadRequest("Nie jesteś adminem eventu");

            if (file == null) return BadRequest("Brak Pliku");
            if (file.Length == 0) return BadRequest("Pusty plik");
            if (file.Length > _photoSettings.MaxBytes) return BadRequest("Za duży plik");
            if (!_photoSettings.IsSupported(file.FileName)) return BadRequest("Nieprawidłowy typ");

            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "uploads");
            await _service.UploadPhoto(eventId, file, uploadsFolderPath);
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