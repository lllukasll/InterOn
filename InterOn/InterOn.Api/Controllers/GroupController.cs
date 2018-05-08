using System.IO;
using System.Threading.Tasks;
using InterOn.Api.Helpers;
using InterOn.Data.ModelsDto.Group;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace InterOn.Api.Controllers
{
    [Authorize]
    [Route("/api/group")]
    public class GroupController : Controller
    {
        private readonly IHostingEnvironment _host;
        private readonly IGroupService _service;

        private readonly PhotoSettings _photoSettings;

        public GroupController(IOptions<PhotoSettings> options, IHostingEnvironment host, IGroupService service)
        {
            _photoSettings = options.Value;
            _host = host;
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupDto groupDto)
        {
            if (!ModelState.IsValid)
                return new UnprocessableEntityObjectResult(ModelState);
            var userId = int.Parse(HttpContext.User.Identity.Name);
            var result = await _service.CreateGroup(groupDto, userId);

            return Ok(result);
        }
        [HttpPost("{groupId}/photo")]
        public async Task<IActionResult> Upload(int groupId, IFormFile file)
        {
            if (await _service.IfExist(groupId) == false) return NotFound();

            if (file == null) return BadRequest("Brak Pliku");
            if (file.Length == 0) return BadRequest("Pusty plik");
            if (file.Length > _photoSettings.MaxBytes) return BadRequest("Za duży plik");
            if (!_photoSettings.IsSupported(file.FileName)) return BadRequest("Nieprawidłowy typ");
          
            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "uploads");
            await _service.UploadPhoto(groupId, file, uploadsFolderPath);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroup(int id, [FromBody] UpdateGroupDto groupDto)
        {
            var userId = int.Parse(HttpContext.User.Identity.Name);
            if (await _service.IfExist(id) == false)
                return NotFound();
            if (await _service.IsAdminAsync(userId,id) == false)
                return BadRequest("Nie jesteś Adminem Grupy");
            if (!ModelState.IsValid)
                return new UnprocessableEntityObjectResult(ModelState);
            var result = await _service.UpdateGroup(groupDto, id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroup(int id)
        {
            var group = await _service.GetGroupMappedAsync(id);
            if (group == null) return NotFound();

            return Ok(group);
        }
        [AllowAnonymous]
        [HttpGet("anonymous/{id}")]
        public async Task<IActionResult> GetGroupAllowAnonymous(int id)
        {
            var group = await _service.GetGroupAllowAnonymousAsync(id);
            if (group == null) return NotFound();

            return Ok(group);
        }

        [HttpGet]
        public async Task<IActionResult> GetGroups()
        {
            var group = await _service.GetGroupsAsync();
            if (group == null) return NotFound();
            return Ok(group);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var userId = int.Parse(HttpContext.User.Identity.Name);
            if (await _service.IfExist(id) == false)
                return NotFound();
            if (await _service.IsAdminAsync(userId, id) == false)
                return BadRequest("Nie jesteś Adminem Grupy");
            await _service.Remove(id);

            return Ok(id);
        }
    }
}