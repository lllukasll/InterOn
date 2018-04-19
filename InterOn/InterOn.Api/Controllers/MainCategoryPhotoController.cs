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
    [Route("/api/maincategories/{mainCategoryId}/photo")]
    public class MainCategoryPhotoController : Controller
    {
        private readonly PhotoSettings _photoSettings;
        private readonly IHostingEnvironment _host;
        private readonly IMainCategoryPhotoService _photoService;
        private readonly IMainCategoryService _mainCategoryService;

        public MainCategoryPhotoController(IHostingEnvironment host,IOptions<PhotoSettings> options,IMainCategoryPhotoService service,IMainCategoryService mainCategoryService)
        {
            _photoSettings = options.Value;
            _host = host;
            _photoService = service;
            _mainCategoryService = mainCategoryService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadPhoto(int mainCategoryId,IFormFile file)
        {
            if (await _mainCategoryService.ExistMainCategory(mainCategoryId) == false) return NotFound();
            
            if (file == null) return BadRequest("Brak Pliku");
            if (file.Length == 0) return BadRequest("Pusty plik");
            if (file.Length > _photoSettings.MaxBytes) return BadRequest("Za duży plik");
            if (!_photoSettings.IsSupported(file.FileName)) return BadRequest("Nieprawidłowy typ");
            if (await _photoService.IsExist(mainCategoryId))
            {
                _photoService.RemovePhoto(mainCategoryId);
            }
            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "uploads");
            var photo = await _photoService.UploadPhoto(mainCategoryId, file, uploadsFolderPath);
            var result = _photoService.MapPhoto(photo);
            return Ok(result);
        }
    }
}