using System.Collections.Generic;
using System.Threading.Tasks;
using InterOn.Data.DbModels;
using InterOn.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace InterOn.Repo.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DataContext _context;

        public GroupRepository(DataContext context)
        {
            _context = context;
        }
        public async void AddAsync(Group group)
        {
           await _context.AddAsync(group);
        }

        public async Task<Group> GetGroup(int id)
        {
            return await _context.Groups
                .Include(g => g.SubCategories)
                .ThenInclude(gc => gc.SubCategory)
                .SingleOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Group>> GetGroups()
        {
            return await _context.Groups
                .Include(g=>g.SubCategories)
                    .ThenInclude(g=>g.SubCategory)
                    .ThenInclude(gmc=>gmc.MainCategory)
                .ToListAsync();
        }

        public void Remove(Group group)
        {
            _context.Remove(group);
        }

      
    }
}