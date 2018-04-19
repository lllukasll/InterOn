using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto.Category;
using InterOn.Repo.Interfaces;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace InterOn.Service.Services
{
    public class SubCategroyPhotoService : ISubCategoryPhotoService
    {
        private readonly IRepository<SubCategoryPhoto> _repository;
        private readonly IMapper _mapper;

        public SubCategroyPhotoService(IRepository<SubCategoryPhoto> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<SubCategoryPhoto> UploadPhoto(int subCategoryId, IFormFile file, string uploadsFolderPath)
        {
            if (!Directory.Exists(uploadsFolderPath)) Directory.CreateDirectory(uploadsFolderPath);
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var photo = new SubCategoryPhoto() { FileName = fileName, SubCategoryRef = subCategoryId };
            await _repository.AddAsyn(photo);
            await _repository.SaveAsync();
            return photo;
        }

        public SubCategoryPhotoDto MapPhoto(SubCategoryPhoto photo)
        {
            var result = _mapper.Map<SubCategoryPhoto, SubCategoryPhotoDto>(photo);
            return result;
        }

        public async Task<bool> IsExist(int id)
        {
            return await _repository.Exist(p => p.SubCategoryRef == id);
        }

        public async void RemovePhoto(int subCategoryId)
        {
            var photo = await _repository.FindBy(mc => mc.SubCategoryRef == subCategoryId).SingleAsync();
            _repository.Remove(photo);
        }
    }
}