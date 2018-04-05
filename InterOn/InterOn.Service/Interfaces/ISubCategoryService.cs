using System.Collections.Generic;
using System.Threading.Tasks;
using InterOn.Data.DbModels;
using InterOn.Repo.Interfaces;

namespace InterOn.Service.Interfaces
{
    public interface ISubCategoryService : IRepository<SubCategory>
    {
        Task<IEnumerable<SubCategory>> GetSubCategoryForMainCategory(int mainCategoryId);
    }
}