using System.Collections.Generic;
using System.Threading.Tasks;
using InterOn.Data.ModelsDto.Category;
using InterOn.Data.ModelsDto.Group;
using Microsoft.AspNetCore.Http;

namespace InterOn.Service.Interfaces
{
    public interface ISubCategoryService
    {
        Task<IEnumerable<GroupUnauthorizedDto>> GetAllGroupsForSubCategory(int categoryId);
        Task<IEnumerable<Data.ModelsDto.Category.SubCategoryDto>> GetAllSubCategoriesAsync();
        Task<bool> ExistSubCategory(int id);
        Task<Data.ModelsDto.Category.SubCategoryDto> UpdateSubCategoryForMainCategory(int subId, int mainId, SaveCategoryDto categoryDto);
        Task<Data.ModelsDto.Category.SubCategoryDto> CreateSubCategoryForMainCategory(int mainId, SaveCategoryDto categoryDto);
        Task<bool> ExistMainCategory(int id);
        Task<IEnumerable<Data.ModelsDto.Category.SubCategoryDto>> GetSubCategoriesForMainCategoryAsync(int mainCategoryId);   
        Task<Data.ModelsDto.Category.SubCategoryDto> GetSubCategoryForMainCategoryAsync(int mainId, int subId);
        Task Remove(int mainId, int subId);
        Task UploadPhoto(int subCategoryId, IFormFile file, string uploadsFolderPath);
    }
}