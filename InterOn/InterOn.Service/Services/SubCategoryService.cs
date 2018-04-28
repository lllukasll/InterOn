﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto.Category;
using InterOn.Data.ModelsDto.Group;
using InterOn.Repo.Interfaces;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Http;

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

        public async Task<IEnumerable<Data.ModelsDto.Category.SubCategoryDto>> GetAllSubCategoriesAsync()
        {
            var categries = await _repository.GetAllSubCategoriesAsync();
            var result = _mapper.Map<IEnumerable<SubCategory>, IEnumerable<Data.ModelsDto.Category.SubCategoryDto>>(categries);
            return result;
        }
        public async Task<bool> ExistMainCategory(int id)
        {
            return await _repository.ExistMainCategoryAsync(id);
        }

        public async Task<IEnumerable<Data.ModelsDto.Category.SubCategoryDto>> GetSubCategoriesForMainCategoryAsync(int mainCategoryId)
        {
            var category = await _repository.GetSubCategoriesForMainCategory(mainCategoryId);
            var result = _mapper.Map<IEnumerable<Data.ModelsDto.Category.SubCategoryDto>>(category);
            return result;
        }

        public async Task<Data.ModelsDto.Category.SubCategoryDto> GetSubCategoryForMainCategoryAsync(int mainId, int subId)
        {
            var subCategory = await _repository.GetSubCategoryForMainCategory(mainId, subId);
            var result = _mapper.Map<Data.ModelsDto.Category.SubCategoryDto>(subCategory);
            return result;
        }

        public async Task Remove(int mainId, int subId)
        {
            var subCategoryFromRepo = await _repository.GetSubCategoryForMainCategory(mainId, subId);
            _repository.Remove(subCategoryFromRepo);
            await _repository.SaveAsync();
        }

        public async Task UploadPhoto(int subCategoryId, IFormFile file, string uploadsFolderPath)
        {
            if (!Directory.Exists(uploadsFolderPath)) Directory.CreateDirectory(uploadsFolderPath);
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var subCategory = await _repository.GetAsync(subCategoryId);
            var photo = new SubCategoryPhotoDto { SubCategoryPhoto = $"{fileName}" };
            _mapper.Map(photo, subCategory);
            await _repository.SaveAsync();
        }

        public async Task<Data.ModelsDto.Category.SubCategoryDto> CreateSubCategoryForMainCategory(int mainId, SaveCategoryDto categoryDto)
        {
            var subCategory = _mapper.Map<SaveCategoryDto, SubCategory>(categoryDto);
            subCategory.MainCategoryId = mainId;
            await _repository.AddAsyn(subCategory);
            await _repository.SaveAsync();
            var result = _mapper.Map<SubCategory, Data.ModelsDto.Category.SubCategoryDto>(subCategory);
            return result;
        }

        public async Task<bool> ExistSubCategory(int id)
        {
            return await _repository.Exist(sc => sc.Id == id);
        }

        public async Task<Data.ModelsDto.Category.SubCategoryDto> UpdateSubCategoryForMainCategory(int subId, int mainId,
            SaveCategoryDto categoryDto)
        {
            var subCategoryFromRepo = await _repository.GetSubCategoryForMainCategory(mainId, subId);
            _mapper.Map(categoryDto, subCategoryFromRepo);
            await _repository.SaveAsync();
            subCategoryFromRepo =
                await _repository.GetSubCategoryForMainCategory(subCategoryFromRepo.Id,
                    subCategoryFromRepo.MainCategoryId);
            var result = _mapper.Map<SubCategory, Data.ModelsDto.Category.SubCategoryDto>(subCategoryFromRepo);
            return result;
        }

        public async Task<IEnumerable<GroupUnauthorizedDto>> GetAllGroupsForSubCategory(int categoryId)
        {
            var groups = await _repository.GetAllGroupForSubCategoryAsync(categoryId);
            var result = _mapper.Map<IEnumerable<Group>, IEnumerable<GroupUnauthorizedDto>>(groups);
            
            return result;

        }
    }
}