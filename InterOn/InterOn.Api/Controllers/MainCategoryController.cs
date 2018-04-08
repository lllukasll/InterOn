using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto.Category;
using InterOn.Repo.Interfaces;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InterOn.Api.Controllers
{
    [Route("/api/maincategories")]
    public class MainCategoryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMainCategoryService _repository;
        private readonly IUnitOfWork _unitOfWork;
        
        public MainCategoryController(IMainCategoryService repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = repository;   
        }

        [HttpPost]
        public async Task<IActionResult> CreateMainCategory([FromBody] SaveCategoryDto saveMainCategoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var categories = _mapper.Map<SaveCategoryDto, MainCategory>(saveMainCategoryDto);
            await _repository.AddAsync(categories);
            await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<MainCategory, SaveCategoryDto>(categories);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMainCategory(int id,[FromBody] SaveCategoryDto saveMainCategoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var category = await _repository.GetMainCategory(id);
            if (category == null)
                return NotFound();
            _mapper.Map(saveMainCategoryDto,category);
            await _unitOfWork.CompleteAsync();
            category = await _repository.GetMainCategory(category.Id);

            var result = _mapper.Map<MainCategory, SaveCategoryDto>(category);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IEnumerable<MainCategoryDto>> GetMainCategories()
        {  
            var categories = await _repository.GetMainCategories();   
            return _mapper.Map<IEnumerable<MainCategory>, IEnumerable<MainCategoryDto>>(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMainCategory(int id)
        {
           var category = await _repository.GetMainCategory(id);
             if (category == null)
                return NotFound();
           var categoryDto = _mapper.Map<MainCategory, MainCategoryDto>(category);
           return Ok(categoryDto);     
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMainCategory(int id)
        {
            var category = await _repository.GetMainCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            _repository.Remove(category);
            await _unitOfWork.CompleteAsync();

            return Ok(id);
        }
    }
}