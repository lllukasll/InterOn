using System.Collections.Generic;
using System.Threading.Tasks;
using InterOn.Data.ModelsDto.Category;
using InterOn.Data.ModelsDto.Group;

namespace InterOn.Service.Interfaces
{
    public interface ISubCategoryService
    {
        Task<IEnumerable<GroupUnauthorizedDto>> GetAllGroupsForSubCategory(int categoryId);
        Task<IEnumerable<SubCategoryDto>> GetAllSubCategoriesAsync();
        Task<bool> ExistSubCategory(int id);
        Task<SubCategoryDto> UpdateSubCategoryForMainCategory(int subId, int mainId, SaveCategoryDto categoryDto);
        Task<SubCategoryDto> CreateSubCategoryForMainCategory(int mainId, SaveCategoryDto categoryDto);
        Task<bool> ExistMainCategory(int id);
        Task<IEnumerable<SubCategoryDto>> GetSubCategoriesForMainCategoryAsync(int mainCategoryId);   
        Task<SubCategoryDto> GetSubCategoryForMainCategoryAsync(int mainId, int subId);
        Task Remove(int mainId, int subId);
    }
}