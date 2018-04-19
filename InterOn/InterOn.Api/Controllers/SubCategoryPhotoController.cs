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
    [Route("/api/maincategories/{mainCategoryId}/subcategories/{subCategoryId}/photo")]
    public class SubCategoryPhotoController : Controller
    {
        private readonly PhotoSettings _photoSettings;
        private readonly ISubCategoryService _subCategoryService;
        private readonly ISubCategoryPhotoService _photoService;
        private readonly IHostingEnvironment _host;

        public SubCategoryPhotoController(ISubCategoryService subCategoryService,ISubCategoryPhotoService photoService,IHostingEnvironment host, IOptions<PhotoSettings> options)
        {
            _photoSettings = options.Value;
            _subCategoryService = subCategoryService;
            _photoService = photoService;
            _host = host;
     
        }
        [HttpPost]
        public async Task<IActionResult> UploadPhoto(int mainCategoryId,int subCategoryId, IFormFile file)
        {
            if (await _subCategoryService.ExistMainCategory(mainCategoryId) == false) return NotFound();
            if (await _subCategoryService.ExistSubCategory(subCategoryId) == false) return NotFound();
            if (file == null) return BadRequest("Brak Pliku");
            if (file.Length == 0) return BadRequest("Pusty plik");
            if (file.Length > _photoSettings.MaxBytes) return BadRequest("Za duży plik");
            if (!_photoSettings.IsSupported(file.FileName)) return BadRequest("Nieprawidłowy typ");
            if (await _photoService.IsExist(subCategoryId))
            {
                _photoService.RemovePhoto(subCategoryId);
            }
            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "uploads");
            var photo = await _photoService.UploadPhoto(subCategoryId, file, uploadsFolderPath);
            var result = _photoService.MapPhoto(photo);
            return Ok(result);
        }
    }
}