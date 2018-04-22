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
        private readonly IGroupPhotoService _photoService;
        private readonly IGroupService _groupService;
        private readonly IHostingEnvironment _host;
        private PhotoSettings _photoSettings;

        public GroupController(IGroupPhotoService photoService, IGroupService groupRepository, IOptions<PhotoSettings> options, IHostingEnvironment host)
        {
            _photoService = photoService;
            _groupService = groupRepository;
            _host = host;
            _photoSettings = options.Value;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupDto groupDto, IFormFile file)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userId = int.Parse(HttpContext.User.Identity.Name);
            var groupId = await _groupService.CreateGroup(groupDto, userId);

            if (await _groupService.IfExist(groupId) == false) return NotFound();

            if (file == null) return BadRequest("Brak Pliku");
            if (file.Length == 0) return BadRequest("Pusty plik");
            if (file.Length > _photoSettings.MaxBytes) return BadRequest("Za duży plik");
            if (!_photoSettings.IsSupported(file.FileName)) return BadRequest("Nieprawidłowy typ");
            if (await _photoService.IsExist(groupId))
            {
                await _photoService.RemovePhoto(groupId);
            }
            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "uploads");
            await _photoService.UploadPhoto(groupId, file, uploadsFolderPath);
            var resultGroup = _groupService.GetGroupMappedAsync(groupId);
            return Ok(resultGroup);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroup(int id, [FromBody] UpdateGroupDto groupDto)
        {
            if (await _groupService.IfExist(id) == false)
                return NotFound();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _groupService.UpdateGroup(groupDto, id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroup(int id)
        {
            var group = await _groupService.GetGroupMappedAsync(id);
            if (group == null) return NotFound();

            return Ok(group);
        }

        [HttpGet]
        public async Task<IActionResult> GetGroups()
        {
            var group = await _groupService.GetGroupsAsync();
            if (group == null) return NotFound();
            return Ok(group);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            if (await _groupService.IfExist(id) == false)
                return NotFound();

            await _groupService.Remove(id);

            return Ok(id);
        }

        [HttpPost("user/{groupId}")]
        public async Task<IActionResult> AddUserForGroup(int groupId)
        {
            if (await _groupService.IfExist(groupId) == false)
                return NotFound();
            var userId = int.Parse(HttpContext.User.Identity.Name);
            if (await _groupService.IfUserBelongToGroupAsync(userId, groupId))
                return BadRequest("Już użytkownik należy do grupy");

            var result = await _groupService.CreateUserGroup(groupId, userId);

            return Ok(result);
        }

        [HttpDelete("user/{groupId}")]
        public async Task<IActionResult> RemoveUserGroup(int groupId)
        {
            var userId = int.Parse(HttpContext.User.Identity.Name);
            if (await _groupService.IfUserBelongToGroupAsync(userId, groupId) == false)
                return BadRequest("Już użytkownik nie należy do grupy");

            await _groupService.RemoveUserGroup(userId, groupId);

            return Ok();
        }
    }
}