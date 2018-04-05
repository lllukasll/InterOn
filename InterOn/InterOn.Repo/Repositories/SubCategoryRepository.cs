using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterOn.Data.DbModels;
using InterOn.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InterOn.Repo.Repositories
{
    public class SubCategoryRepository: Repository<SubCategory>, ISubCategoryRepository
    {

        public SubCategoryRepository(DataContext context) : base(context)
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