using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using InterOn.Data.DbModels;
using InterOn.Repo.Interfaces;

namespace InterOn.Service.Interfaces
{
    public interface ISubCategoryService
    {
        Task<bool> ExistMainCategoryAsync(int id);
        Task<IEnumerable<SubCategory>> GetSubCategoriesForMainCategoryAsync(int mainCategoryId);
        Task AddAsync(SubCategory category);
        Task<SubCategory> GetSubCategoryForMainCategoryAsync(int mainId, int subId);
        void Add(SubCategory category);
        void Remove(SubCategory category);
    }
}