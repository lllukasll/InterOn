using System.Collections.Generic;
using System.Threading.Tasks;
using InterOn.Data.DbModels;

namespace InterOn.Repo.Interfaces
{
    public interface ISubCategoryRepository:IRepository<SubCategory>
    {
        Task<IEnumerable<SubCategory>> GetSubCategoriesForMainCategory(int mainCategoryId);
        Task<bool> ExistSubCategoryAsync(int id);
        void Added(SubCategory subCategory);
        Task<SubCategory> GetSubCategoryForMainCategory(int mainId, int subId);
    }
}