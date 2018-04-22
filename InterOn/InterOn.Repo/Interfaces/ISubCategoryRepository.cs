using System.Collections.Generic;
using System.Threading.Tasks;
using InterOn.Data.DbModels;

namespace InterOn.Repo.Interfaces
{
    public interface ISubCategoryRepository:IRepository<SubCategory>
    {
        Task<IEnumerable<Group>> GetAllGroupForSubCategoryAsync(int categoryId);
        Task<IEnumerable<SubCategory>> GetAllSubCategoriesAsync();
        Task<IEnumerable<SubCategory>> GetSubCategoriesForMainCategory(int mainCategoryId);
        Task<bool> ExistMainCategoryAsync(int id);
        Task<SubCategory> GetSubCategoryForMainCategory(int mainId, int subId);
    }
}