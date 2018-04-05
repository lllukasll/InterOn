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
    public class MainCategoryService :  IMainCategoryService
    {
        private readonly IMainCategoryRepository _repository;


        public MainCategoryService(IMainCategoryRepository repository)
        {
            _repository = repository;
        }   
        public async Task<MainCategory> GetMainCategory(int id, bool includeRelated = true)
        {
            if (!includeRelated == true)
                return await _repository.GetAsync(id);
            return await _repository.GetMainCategory(id);
        }

        public async Task<IEnumerable<MainCategory>> GetMainCategories()
        {
            return await _repository.GetMainCategories();
        }

        public bool ExistMainCategory(int id)
        {
            return _repository.ExistMainCategory(id);
        }
    }
}