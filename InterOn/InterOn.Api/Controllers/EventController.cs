using System.IO;
using InterOn.Data.ModelsDto.Event;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using InterOn.Api.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace InterOn.Api.Controllers
{
    [Authorize]
    [Route("api/event")]
    public class EventController : Controller
    {
        private readonly IHostingEnvironment _host;
        private readonly IEventService _service;
        private readonly PhotoSettings _photoSettings;


        public EventController(IOptions<PhotoSettings> options, IHostingEnvironment host, IEventService service)
        {
            _host = host;
            _service = service;
            _photoSettings = options.Value;
        }

        [HttpPost]
        [Route("{eventId}/photo")]
        public async Task<IActionResult> Upload(int eventId, IFormFile file)
        {
            var userId = int.Parse(HttpContext.User.Identity.Name);
            if (await _service.ExistEvent(eventId) == false) return NotFound();
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
        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventDto eventDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userId = int.Parse(HttpContext.User.Identity.Name);
            var result =  await _service.CreateEventAsync(userId,eventDto);
            
            return CreatedAtRoute("GetEvent",new {eventId=result.Id},result);
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

        [HttpGet("{eventId}",Name = "GetEvent")]
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