using System.IO;
using System.Threading.Tasks;
using InterOn.Api.Helpers;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace InterOn.Api.Controllers
{
    [Route("api/group/{groupId}/photo")]
    public class GroupPhotoController : Controller
    {
        private readonly IHostingEnvironment _host;
        private readonly IGroupService _groupService;
        private readonly IGroupPhotoService _photoService;
        private readonly PhotoSettings _photoSettings;
        
        public GroupPhotoController(IOptions<PhotoSettings> options, IHostingEnvironment host,IGroupService groupService,IGroupPhotoService photoService)
        {
            _photoSettings = options.Value;    
            _host = host;
            _groupService = groupService;
            _photoService = photoService;
        }
        [HttpPost]
        public async Task<IActionResult> Upload(int groupId, IFormFile file)
        {
           
            var group = _groupService.GetGroupAsync(groupId, false);
                if (group == null)
                    return NotFound();

            var photo1 = _photoService.IsExist(groupId);
                if (photo1) return BadRequest("Przy tej grupie już jest avatar");
                if (file == null) return BadRequest("Brak Pliku");
                if (file.Length == 0) return BadRequest("Pusty plik");
                if (file.Length > _photoSettings.MaxBytes) return BadRequest("Za duży plik");
                if (!_photoSettings.IsSupported(file.FileName)) return BadRequest("Nieprawidłowy typ");

            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "uploads");
            var photo = await _photoService.UploadPhoto(groupId, file, uploadsFolderPath);
            var result = _photoService.MapPhoto(photo);

            return Ok(result);
        }
    }
}