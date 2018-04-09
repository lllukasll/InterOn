using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterOn.Data.DbModels;
using InterOn.Repo;
using InterOn.Repo.Interfaces;
using InterOn.Repo.Repositories;
using InterOn.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InterOn.Service.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ISubCategoryRepository _repository;

        public SubCategoryService(ISubCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> ExistMainCategoryAsync(int id)
        {
            return await _repository.ExistSubCategoryAsync(id);
        }
        

        public async Task<IEnumerable<SubCategory>> GetSubCategoriesForMainCategoryAsync(int mainCategoryId)
        {
            return await _repository.GetSubCategoriesForMainCategory(mainCategoryId);
        }

        public async Task AddAsync(SubCategory category)
        {
            await _repository.AddAsyn(category);
        }

        public async Task<SubCategory> GetSubCategoryForMainCategoryAsync(int mainId, int subId)
        {
            return await _repository.GetSubCategoryForMainCategory(mainId, subId);
        }

        public void Add(SubCategory category)
        {
            _repository.Added(category);
        }

        public void Remove(SubCategory category)
        {
            _repository.Remove(category);
        }
    }
}