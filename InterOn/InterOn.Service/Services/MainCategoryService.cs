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
    public class MainCategoryService : Repository<MainCategory>, IMainCategoryService
    {
        

        public MainCategoryService(DataContext context) : base(context)
        {
            
        }   
        public async Task<MainCategory> GetMainCategory(int id, bool includeRelated = true)
        {
            if (!includeRelated == true)
                return await GetAsync(id);
            return await _context.Set<MainCategory>().Include(s => s.SubCategories).SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<MainCategory>> GetMainCategories()
        {  
            return await _context.Set<MainCategory>().Include(s=>s.SubCategories).ToListAsync();
        }

        public bool ExistMainCategory(int id)
        {
            return _context.MainCategories.Any(a => a.Id == id);
        }
    }
}