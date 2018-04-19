using System.Collections.Generic;
using System.Threading.Tasks;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto.Category;

namespace InterOn.Service.Interfaces
{
    public interface IMainCategoryService
    {
        Task<SaveCategoryDto> UpdateMainCategory(int id, SaveCategoryDto categoryDto);
        Task<SaveCategoryDto> CreateMainCategory(SaveCategoryDto categoryDto);
        Task<bool> ExistMainCategory(int id);
        Task<MainCategoryDto> GetMainCategory(int id);       
        Task<IEnumerable<MainCategoryDto>> GetMainCategories();
        Task AddAsync(MainCategory category);
        void Remove(int id);
    }
}