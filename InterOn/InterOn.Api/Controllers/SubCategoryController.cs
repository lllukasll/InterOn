using System.Threading.Tasks;
using InterOn.Data.ModelsDto.Category;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InterOn.Api.Controllers
{
    [Route("/api/maincategories/{mainId}/subcategories")]
    public class SubCategoryController : Controller
    {
        private readonly ISubCategoryService _service;

        public SubCategoryController(ISubCategoryService repository)
        {
            _service = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetSubCategoriesForMainCategory(int mainId)
        {
            if (_service.ExistMainCategory(mainId) == false)
            {
                return NotFound();
            }

            var result = await _service.GetSubCategoriesForMainCategoryAsync(mainId);
            return Ok(result);
        }

        [HttpGet("{subId}")]
        public async Task<IActionResult> GetSubCategoryForMainCategory(int mainId, int subId)
        {
            if (_service.ExistMainCategory(mainId) == false)
            {
                return BadRequest("Nie ma MainCategory o tym Id");
            }

            var result = await _service.GetSubCategoryForMainCategoryAsync(mainId, subId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubCategory(int mainId, [FromBody] SaveCategoryDto category)
        {
            if (_service.ExistMainCategory(mainId) == false)
            {
                return BadRequest("Nie ma MainCategory o tym Id");
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _service.CreateSubCategoryForMainCategory(mainId, category);
            return Ok(result);
        }

        [HttpPut("{subId}")]
        public async Task<IActionResult> UpdateSubCategory(int mainId, int subId,
            [FromBody] SaveCategoryDto categoryDto)
        {
            if (_service.ExistMainCategory(mainId) == false)
            {
                return BadRequest("Nie ma MainCategory o tym Id");
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _service.UpdateSubCategoryForMainCategory(subId, mainId, categoryDto);
            return Ok(result);
        }

        [HttpDelete("{subId}")]
        public IActionResult DeleteSubCategory(int subId, int mainId)
        {
            if (_service.ExistMainCategory(mainId) == false || _service.ExistSubCategory(subId) == false)
            {
                return BadRequest("Nie ma MainCategory lub SubCategory o tym Id ");
            }

            _service.Remove(mainId, subId);
            return Ok(subId);
        }
    }
}