using System.IO;
using InterOn.Data.ModelsDto.Category;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using InterOn.Api.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace InterOn.Api.Controllers
{

    public class SubCategoryController : Controller
    {
        private readonly PhotoSettings _photoSettings;
        private readonly IHostingEnvironment _host;
        private readonly ISubCategoryService _service;

        public SubCategoryController(ISubCategoryService subCategoryService, IHostingEnvironment host, IOptions<PhotoSettings> options)
        {
            _photoSettings = options.Value;
            _service = subCategoryService;
            _host = host;
        }
        [HttpPost("/api/maincategories/{mainCategoryId}/subcategories/{subCategoryId}/photo")]
        public async Task<IActionResult> UploadPhoto(int mainCategoryId, int subCategoryId, IFormFile file)
        {
            if (await _service.ExistMainCategory(mainCategoryId) == false) return NotFound();
            if (await _service.ExistSubCategory(subCategoryId) == false) return NotFound();
            if (file == null) return BadRequest("Brak Pliku");
            if (file.Length == 0) return BadRequest("Pusty plik");
            if (file.Length > _photoSettings.MaxBytes) return BadRequest("Za duży plik");
            if (!_photoSettings.IsSupported(file.FileName)) return BadRequest("Nieprawidłowy typ");
       
            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "uploads");
            await _service.UploadPhoto(subCategoryId, file, uploadsFolderPath);
            return Ok();
        }

        [HttpGet("/api/maincategories/{mainId}/subcategories")]
        public async Task<IActionResult> GetSubCategoriesForMainCategory(int mainId)
        {
            if (await _service.ExistMainCategory(mainId) == false)
            {
                return NotFound();
            }

            var result = await _service.GetSubCategoriesForMainCategoryAsync(mainId);
            return Ok(result);
        }

        [HttpGet("/api/maincategories/{mainId}/subcategories/{subId}")]
        public async Task<IActionResult> GetSubCategoryForMainCategory(int mainId, int subId)
        {
            if (await _service.ExistMainCategory(mainId) == false)
            {
                return BadRequest("Nie ma MainCategory o tym Id");
            }

            var result = await _service.GetSubCategoryForMainCategoryAsync(mainId, subId);
            return Ok(result);
        }

        [HttpPost("/api/maincategories/{mainId}/subcategories")]
        public async Task<IActionResult> CreateSubCategory(int mainId, [FromBody] SaveCategoryDto category)
        {
            if (await _service.ExistMainCategory(mainId) == false)
            {
                return BadRequest("Nie ma MainCategory o tym Id");
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _service.CreateSubCategoryForMainCategory(mainId, category);
            return Ok(result);
        }

        [HttpPut("/api/maincategories/{mainId}/subcategories/{subId}")]
        public async Task<IActionResult> UpdateSubCategory(int mainId, int subId,
            [FromBody] SaveCategoryDto categoryDto)
        {
            if (await _service.ExistMainCategory(mainId) == false)
            {
                return BadRequest("Nie ma MainCategory o tym Id");
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _service.UpdateSubCategoryForMainCategory(subId, mainId, categoryDto);
            return Ok(result);
        }

        [HttpDelete("/api/maincategories/{mainId}/subcategories/{subId}")]
        public async Task<IActionResult> DeleteSubCategory(int subId, int mainId)
        {
            if (await _service.ExistMainCategory(mainId) == false || await _service.ExistSubCategory(subId) == false)
            {
                return BadRequest("Nie ma MainCategory lub SubCategory o tym Id ");
            }

            await _service.Remove(mainId, subId);
            return Ok(subId);
        }
        [HttpGet("/api/subcategories")]
        public async Task<IActionResult> GetAllSubCategories()
        {
            var categories = await _service.GetAllSubCategoriesAsync();
            if (categories == null)
                return NotFound();
            return Ok(categories);
        }

        [HttpGet("/api/subcategories/{categoryId}/group")]
        public async Task<IActionResult> GetGroupForSubCategories(int categoryId)
        {
            var group = await _service.GetAllGroupsForSubCategory(categoryId);
            if (group == null) return NotFound();
            return Ok(group);
        }
    }
}