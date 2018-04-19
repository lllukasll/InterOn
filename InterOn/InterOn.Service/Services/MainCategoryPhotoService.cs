using System;
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
    public class MainCategoryPhotoService : IMainCategoryPhotoService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<MainCategoryPhoto> _repository;

        public MainCategoryPhotoService(IMapper mapper,IRepository<MainCategoryPhoto> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<MainCategoryPhoto> UploadPhoto(int groupId, IFormFile file, string uploadsFolderPath)
        {
            if (!Directory.Exists(uploadsFolderPath)) Directory.CreateDirectory(uploadsFolderPath);
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var photo = new MainCategoryPhoto { FileName = fileName, MainCategoryRef = groupId };
            await _repository.AddAsyn(photo);
            await _repository.SaveAsync();
            return photo;
        }

        public MainCategoryPhotoDto MapPhoto(MainCategoryPhoto photo)
        {
            var result = _mapper.Map<MainCategoryPhoto, MainCategoryPhotoDto>(photo);
            return result;
        }

        public async Task<bool> IsExist(int id)
        {
            return await _repository.Exist(p => p.MainCategoryRef == id);
        }
    }
}