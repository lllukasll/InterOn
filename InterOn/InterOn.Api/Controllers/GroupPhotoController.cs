using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InterOn.Api.Helpers;
using InterOn.Data.DbModels;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace InterOn.Api.Controllers
{
    
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
        [Route("api/group/{groupId}/photo")]
        public async Task<IActionResult> Upload(int groupId, IFormFile file)
        {
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
            var photo = await _photoService.UploadPhoto(groupId, file, uploadsFolderPath);
            var result = _photoService.MapPhoto(photo);
            return Ok(result);
        }


        //GET api/file/id
        [HttpGet]
        [Route("api/photo/{fileName}")]
        public async Task<IActionResult> GetFile(string fileName)
        {
            if (fileName == null || fileName == "null")
                return BadRequest();

            var stream = _host.WebRootPath + "\\uploads\\" + fileName;
            var imageFileStream = System.IO.File.OpenRead(stream);
            return File(imageFileStream, "image/jpeg");
        }
        /*
        [HttpGet]
        public async Task<IActionResult> GetGroupPhoto(int groupId)
        {

            if (await _groupService.IfExist(groupId) == false)
                return NotFound();
            var photo = await _photoService.GetGroupPhoto(groupId);
            
            var result = _photoService.MapPhotoDtoQueryable(photo);

            return Ok(result);
        }
        */
    }
}