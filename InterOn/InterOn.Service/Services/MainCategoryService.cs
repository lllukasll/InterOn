using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto.Category;
using InterOn.Repo.Interfaces;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Http;

namespace InterOn.Service.Services
{
    public class MainCategoryService : IMainCategoryService
    {
        private readonly IMainCategoryRepository _repository;
        private readonly IMapper _mapper;

        public MainCategoryService(IMainCategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MainCategoryDto> GetMainCategory(int id)
        {
            var category = await _repository.GetMainCategory(id);
            var categoryDto = _mapper.Map<MainCategory, MainCategoryDto>(category);
            return categoryDto;
        }

        public async Task<IEnumerable<MainCategoryDto>> GetMainCategories()
        {
            var categories = await _repository.GetMainCategories();
            var result = _mapper.Map<IEnumerable<MainCategory>, IEnumerable<MainCategoryDto>>(categories);
            return result;
        }

        public async Task<bool> ExistMainCategory(int id)
        {
            return await _repository.Exist(a => a.Id == id);
        }

        public async Task AddAsync(MainCategory category)
        {
            await _repository.AddAsyn(category);
        }

        public async Task Remove(int id)
        {
            var category = await _repository.GetAsync(id);
            _repository.Remove(category);
            await _repository.SaveAsync();
        }

        public async Task UploadPhoto(int mainCategoryId, IFormFile file, string uploadsFolderPath)
        {
            if (!Directory.Exists(uploadsFolderPath)) Directory.CreateDirectory(uploadsFolderPath);
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var mainCategory = await _repository.GetAsync(mainCategoryId);
            var photo = new MainCategory { MainCategoryPhoto = $"{fileName}" };
            _mapper.Map(photo, mainCategory);
            await _repository.SaveAsync();
        }

        public async Task<SaveCategoryDto> CreateMainCategory(SaveCategoryDto categoryDto)
        {
            var categories = _mapper.Map<SaveCategoryDto, MainCategory>(categoryDto);
            await _repository.AddAsyn(categories);
            await _repository.SaveAsync();
            var result = _mapper.Map<MainCategory, SaveCategoryDto>(categories);
            return result;
        }

        public async Task<SaveCategoryDto> UpdateMainCategory(int id, SaveCategoryDto categoryDto)
        {
            var category = await _repository.GetMainCategory(id);
            _mapper.Map(categoryDto, category);
            await _repository.SaveAsync();
            category = await _repository.GetMainCategory(category.Id);
            var result = _mapper.Map<MainCategory, SaveCategoryDto>(category);
            return result;
        }
    }
}