using System.Threading.Tasks;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto.Category;
using Microsoft.AspNetCore.Http;

namespace InterOn.Service.Interfaces
{
    public interface IMainCategoryPhotoService
    {
        Task<MainCategoryPhoto> UploadPhoto(int groupId, IFormFile file, string uploadsFolderPath);
        MainCategoryPhotoDto MapPhoto(MainCategoryPhoto photo);
        Task<bool> IsExist(int id);
        Task RemovePhoto(int mainCategoryId);
    }
}