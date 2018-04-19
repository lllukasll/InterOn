using System.Threading.Tasks;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto.Category;
using Microsoft.AspNetCore.Http;

namespace InterOn.Service.Interfaces
{
    public interface ISubCategoryPhotoService
    {
        Task<SubCategoryPhoto> UploadPhoto(int subCategoryId, IFormFile file, string uploadsFolderPath);
        SubCategoryPhotoDto MapPhoto(SubCategoryPhoto photo);
        Task<bool> IsExist(int id);
        void RemovePhoto(int subCategoryId);
    }
}