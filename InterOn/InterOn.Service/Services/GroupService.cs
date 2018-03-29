using System.Collections.Generic;
using System.Threading.Tasks;
using InterOn.Data.DbModels;
using InterOn.Repo;
using InterOn.Repo.Interfaces;
using InterOn.Repo.Repositories;
using InterOn.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InterOn.Service.Services
{
    public class GroupService : Repository<Group>, IGroupService 
    {
        public GroupService(DataContext context):base(context)
        {
            
        }
        public async Task<Group> GetGroup(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await _context.Groups.FindAsync(id);

            return await _context.Groups
                .Include(g => g.SubCategories)
                .ThenInclude(gc => gc.SubCategory)
                .SingleOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Group>> GetGroups()
        {
            return await _context.Groups
                .Include(g => g.SubCategories)
                .ThenInclude(g => g.SubCategory)
                .ThenInclude(gmc => gmc.MainCategory)
                .ToListAsync();
        }
    }
}