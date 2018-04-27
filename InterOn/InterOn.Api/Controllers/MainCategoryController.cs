using System.IO;
using System.Threading.Tasks;
using InterOn.Api.Helpers;
using InterOn.Data.ModelsDto.Category;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace InterOn.Api.Controllers
{
    [Route("/api/maincategories")]
    public class MainCategoryController : Controller
    {
        private readonly PhotoSettings _photoSettings;
        private readonly IHostingEnvironment _host;
        private readonly IMainCategoryService _mainCategoryService;

        public MainCategoryController(IHostingEnvironment host, IOptions<PhotoSettings> options, IMainCategoryService mainCategoryService)
        {
            _photoSettings = options.Value;
            _host = host;
            _mainCategoryService = mainCategoryService;
        }

        [HttpPost("{mainCategoryId}/photo")]
        public async Task<IActionResult> UploadPhoto(int mainCategoryId, IFormFile file)
        {
            if (await _mainCategoryService.ExistMainCategory(mainCategoryId) == false) return NotFound();

            if (file == null) return BadRequest("Brak Pliku");
            if (file.Length == 0) return BadRequest("Pusty plik");
            if (file.Length > _photoSettings.MaxBytes) return BadRequest("Za duży plik");
            if (!_photoSettings.IsSupported(file.FileName)) return BadRequest("Nieprawidłowy typ");
            
            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "uploads");
            await _mainCategoryService.UploadPhoto(mainCategoryId, file, uploadsFolderPath);
            
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMainCategory([FromBody] SaveCategoryDto saveMainCategoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _mainCategoryService.CreateMainCategory(saveMainCategoryDto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMainCategory(int id, [FromBody] SaveCategoryDto saveMainCategoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (await _mainCategoryService.ExistMainCategory(id) == false) return NotFound();
            var result = await _mainCategoryService.UpdateMainCategory(id, saveMainCategoryDto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetMainCategories()
        {
            var result = await _mainCategoryService.GetMainCategories();
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMainCategory(int id)
        {
            if (await _mainCategoryService.ExistMainCategory(id) == false) return NotFound();
            var result = await _mainCategoryService.GetMainCategory(id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMainCategory(int id)
        {
            if (await _mainCategoryService.ExistMainCategory(id) == false) return NotFound();
            await _mainCategoryService.Remove(id);
            return Ok(id);
        }
    }
}