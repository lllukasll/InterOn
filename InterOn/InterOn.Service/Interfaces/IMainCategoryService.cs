using System.Collections.Generic;
using System.Threading.Tasks;
using InterOn.Data.DbModels;
using InterOn.Repo.Interfaces;

namespace InterOn.Service.Interfaces
{
    public interface IMainCategoryService : IRepository<MainCategory>
    {


        Task<MainCategory> GetMainCategory(int id, bool includeRelated = true);       
        Task<IEnumerable<MainCategory>> GetMainCategories();

    }
}