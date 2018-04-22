using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto.Group;
using Microsoft.AspNetCore.Http;

namespace InterOn.Service.Interfaces
{
    public interface IGroupPhotoService
    {
        Task RemovePhoto(int groupId);
        Task<GroupPhoto> GetPhoto(string fileName);
        Task<GroupPhoto> GetGroupPhoto(int id);
        Task<bool> IsExist(int id);
        Task<GroupPhoto> UploadPhoto(int groupId, IFormFile file, string uploadsFolderPath);
        GroupPhotoDto MapPhoto(GroupPhoto photo);
    }
}