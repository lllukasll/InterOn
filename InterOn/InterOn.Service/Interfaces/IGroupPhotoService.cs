using System.Threading.Tasks;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto;
using InterOn.Data.ModelsDto.Group;
using Microsoft.AspNetCore.Http;

namespace InterOn.Service.Interfaces
{
    public interface IGroupPhotoService
    {
        bool IsExist(int id);
        Task<GroupPhoto> UploadPhoto(int groupId, IFormFile file, string uploadsFolderPath);
        GroupPhotoDto MapPhoto(GroupPhoto photo);
    }
}