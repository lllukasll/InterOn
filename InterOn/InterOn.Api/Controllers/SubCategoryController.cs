using System.Threading.Tasks;
using InterOn.Data.ModelsDto.Category;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InterOn.Api.Controllers
{
    
    public class SubCategoryController : Controller
    {
        private readonly ISubCategoryService _service;

        public SubCategoryController(ISubCategoryService repository)
        {
            _service = repository;
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
    }
}