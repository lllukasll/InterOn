using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterOn.Data.DbModels;
using InterOn.Repo;
using InterOn.Repo.Repositories;
using InterOn.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InterOn.Service.Services
{
    public class SubCategoryService : Repository<SubCategory>, ISubCategoryService
    {
        public SubCategoryService(DataContext context):base(context)
        {
            
        }
        public async Task<IEnumerable<SubCategory>> GetSubCategoryForMainCategory(int mainCategoryId)
        {
           return await _context.Set<SubCategory>()
                .Where(sc => sc.MainCategoryId == mainCategoryId)
                .OrderBy(o => o.Name)
                .ToListAsync();
        }
    }
}