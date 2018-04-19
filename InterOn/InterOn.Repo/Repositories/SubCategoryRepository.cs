using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterOn.Data.DbModels;
using InterOn.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InterOn.Repo.Repositories
{
    public class SubCategoryRepository : Repository<SubCategory>, ISubCategoryRepository
    {
        public SubCategoryRepository(DataContext context) : base(context)
        {
        }

        public async Task<IEnumerable<SubCategory>> GetSubCategoriesForMainCategory(int mainCategoryId)
        {
            return await _context.Set<SubCategory>()
                .Include(p=>p.SubCategoryPhoto)
                .Where(sc => sc.MainCategoryId == mainCategoryId)
                .OrderBy(o => o.Name)
                .ToListAsync();
        }

        public async Task<bool> ExistMainCategoryAsync(int id)
        {
            return await _context.Set<MainCategory>().AnyAsync(a => a.Id == id);
        }

        public async Task<SubCategory> GetSubCategoryForMainCategory(int mainId, int subId)
        {
            return await _context.Set<SubCategory>()
                .Include(p=>p.SubCategoryPhoto)
                .Where(s => s.Id == subId && s.MainCategoryId == mainId)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<SubCategory>> GetAllSubCategoriesAsync()
        {
            return await _context.Set<SubCategory>()
                .Include(p => p.SubCategoryPhoto)
                .ToListAsync();
        }
    }
}