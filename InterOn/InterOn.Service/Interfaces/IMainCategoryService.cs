using System.Collections.Generic;
using System.Threading.Tasks;
using InterOn.Data.DbModels;
using InterOn.Repo.Interfaces;

namespace InterOn.Service.Interfaces
{
    public interface IMainCategoryService
    {
        bool ExistMainCategory(int id);
        Task<MainCategory> GetMainCategory(int id, bool includeRelated = true);       
        Task<IEnumerable<MainCategory>> GetMainCategories();
        Task AddAsync(MainCategory category);
        void Remove(MainCategory category);
    }
}