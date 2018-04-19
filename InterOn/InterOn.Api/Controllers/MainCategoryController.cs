using System.Threading.Tasks;
using InterOn.Data.ModelsDto.Category;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InterOn.Api.Controllers
{
    [Route("/api/maincategories")]
    public class MainCategoryController : Controller
    {
        private readonly IMainCategoryService _repository;

        public MainCategoryController(IMainCategoryService repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMainCategory([FromBody] SaveCategoryDto saveMainCategoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _repository.CreateMainCategory(saveMainCategoryDto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMainCategory(int id, [FromBody] SaveCategoryDto saveMainCategoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (await _repository.ExistMainCategory(id) == false) return NotFound();
            var result = await _repository.UpdateMainCategory(id, saveMainCategoryDto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetMainCategories()
        {
            var result = await _repository.GetMainCategories();
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMainCategory(int id)
        {
            if (await _repository.ExistMainCategory(id) == false) return NotFound();
            var result = await _repository.GetMainCategory(id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMainCategory(int id)
        {
            if (await _repository.ExistMainCategory(id) == false) return NotFound();
            _repository.Remove(id);
            return Ok(id);
        }
    }
}