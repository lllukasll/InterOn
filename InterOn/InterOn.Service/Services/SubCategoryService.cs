using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto.Category;
using InterOn.Repo.Interfaces;
using InterOn.Service.Interfaces;

namespace InterOn.Service.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ISubCategoryRepository _repository;
        private readonly IMapper _mapper;

        public SubCategoryService(ISubCategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public bool ExistMainCategory(int id)
        {
            return _repository.ExistMainCategory(id);
        }

        public async Task<IEnumerable<SubCategoryDto>> GetSubCategoriesForMainCategoryAsync(int mainCategoryId)
        {
            var category = await _repository.GetSubCategoriesForMainCategory(mainCategoryId);
            var result = _mapper.Map<IEnumerable<SubCategoryDto>>(category);
            return result;
        }

        public async Task<SubCategoryDto> GetSubCategoryForMainCategoryAsync(int mainId, int subId)
        {
            var subCategory = await _repository.GetSubCategoryForMainCategory(mainId, subId);
            var result = _mapper.Map<SubCategoryDto>(subCategory);
            return result;
        }

        public async void Remove(int mainId, int subId)
        {
            var subCategoryFromRepo = await _repository.GetSubCategoryForMainCategory(mainId, subId);
            _repository.Remove(subCategoryFromRepo);
            await _repository.SaveAsync();
        }

        public async Task<SubCategoryDto> CreateSubCategoryForMainCategory(int mainId, SaveCategoryDto categoryDto)
        {
            var subCategory = _mapper.Map<SaveCategoryDto, SubCategory>(categoryDto);
            subCategory.MainCategoryId = mainId;
            await _repository.AddAsyn(subCategory);
            await _repository.SaveAsync();
            var result = _mapper.Map<SubCategory, SubCategoryDto>(subCategory);
            return result;
        }

        public bool ExistSubCategory(int id)
        {
            return _repository.Exist(sc => sc.Id == id);
        }

        public async Task<SubCategoryDto> UpdateSubCategoryForMainCategory(int subId, int mainId,
            SaveCategoryDto categoryDto)
        {
            var subCategoryFromRepo = await _repository.GetSubCategoryForMainCategory(mainId, subId);
            _mapper.Map(categoryDto, subCategoryFromRepo);
            await _repository.SaveAsync();
            subCategoryFromRepo =
                await _repository.GetSubCategoryForMainCategory(subCategoryFromRepo.Id,
                    subCategoryFromRepo.MainCategoryId);
            var result = _mapper.Map<SubCategory, SubCategoryDto>(subCategoryFromRepo);
            return result;
        }
    }
}