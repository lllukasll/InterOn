using System.Collections.Generic;
using System.Threading.Tasks;
using InterOn.Data.DbModels;

namespace InterOn.Repo.Interfaces
{
    public interface ISubCategoryRepository:IRepository<SubCategory>
    {
        Task<IEnumerable<SubCategory>> GetSubCategoryForMainCategory(int mainCategoryId);
    }
}