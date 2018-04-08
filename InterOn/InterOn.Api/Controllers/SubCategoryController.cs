using System;
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
    [Route("/api/maincategories/{mainId}/subcategories")]
    public class SubCategoryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISubCategoryService _repository;

        public SubCategoryController(IMapper mapper, ISubCategoryService repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetSubCategoriesForMainCategory(int mainId)
        {
            if (await _repository.ExistMainCategoryAsync(mainId)==false)
            {
                return NotFound();
            }
            var category = await _repository.GetSubCategoriesForMainCategoryAsync(mainId);
            var result = _mapper.Map<IEnumerable<SubCategoryDto>>(category);

            return Ok(result);
        }

        [HttpGet("{subId}")]
        public async Task<IActionResult> GetSubCategoryForMainCategory(int mainId, int subId)
        {
            if (_repository.ExistMainCategoryAsync(mainId) == null)
            {
                return NotFound("Nie ma MainCategory o tym Id");
            }
            var subCategory = await _repository.GetSubCategoryForMainCategoryAsync(mainId, subId);
            var result = _mapper.Map<SubCategoryDto>(subCategory);

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSubCategory(int mainId, [FromBody] SaveCategoryDto category)
        {
            try
            {
                if (_repository.ExistMainCategoryAsync(mainId) == null)
                {
                    return BadRequest("Nie ma MainCategory o tym Id");
                }
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var subCategory = _mapper.Map<SaveCategoryDto, SubCategory>(category);
                subCategory.MainCategoryId = mainId;
                await _repository.AddAsync(subCategory);
                await _unitOfWork.CompleteAsync();

                var result = _mapper.Map<SubCategory, SubCategoryDto>(subCategory);

                return Ok(result);

            }
            catch (Exception e)
            {
                return BadRequest($"{e.Message}");
            }
           
        }
        [HttpPut("{subId}")]
        public async Task<IActionResult> UpdateSubCategory(int mainId,int subId, [FromBody] SaveCategoryDto categoryDto)
        {
            try
            {
                if (_repository.ExistMainCategoryAsync(mainId) == null)
                {
                    return BadRequest("Nie ma MainCategory o tym Id");
                }
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var subCategoryFromRepo = await _repository.GetSubCategoryForMainCategoryAsync(mainId, subId);
                if (subCategoryFromRepo == null)
                {
                    return NotFound();
                }
                _mapper.Map(categoryDto,subCategoryFromRepo);
                await _unitOfWork.CompleteAsync();
                subCategoryFromRepo = await _repository.GetSubCategoryForMainCategoryAsync(subCategoryFromRepo.Id, subCategoryFromRepo.MainCategoryId);

                var result = _mapper.Map<SubCategory, SaveCategoryDto>(subCategoryFromRepo);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest($"{e.Message}");
            }
        }

        [HttpDelete("{subId}")]
        public async Task<IActionResult> DeleteSubCategory(int subId,int mainId)
        {
           var subCategoryFromRepo = await _repository.GetSubCategoryForMainCategoryAsync(mainId, subId);
            if (subCategoryFromRepo==null)
            {
                return NotFound();
            }
            _repository.Remove(subCategoryFromRepo);

            await _unitOfWork.CompleteAsync();
            return Ok(subId);
        }
    }
}