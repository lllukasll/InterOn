using System.Collections.Generic;
using System.Threading.Tasks;
using InterOn.Data.DbModels;

namespace InterOn.Repo.Interfaces
{
    public interface ISubCategoryRepository:IRepository<SubCategory>
    {
        Task<IEnumerable<SubCategory>> GetSubCategoriesForMainCategory(int mainCategoryId);
        bool ExistMainCategory(int id);
        Task<SubCategory> GetSubCategoryForMainCategory(int mainId, int subId);
    }
}