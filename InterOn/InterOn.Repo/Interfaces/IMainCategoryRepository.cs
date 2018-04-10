using System.Collections.Generic;
using System.Threading.Tasks;
using InterOn.Data.DbModels;

namespace InterOn.Repo.Interfaces
{
    public interface IMainCategoryRepository : IRepository<MainCategory>
    {
        Task<MainCategory> GetMainCategory(int id, bool includeRelated = true);
        Task<IEnumerable<MainCategory>> GetMainCategories();
    }
}