using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterOn.Data.DbModels;
using InterOn.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InterOn.Repo.Repositories
{
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        public GroupRepository(DataContext context) : base(context)
        {

        }

        public void RemoveUserGroup(UserGroup userGroup)
        {
            _context.Set<UserGroup>().Remove(userGroup);
        }

        public async Task<Group> GetGroup(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await _context.Groups.FindAsync(id);

            return await _context.Groups
                .Include(u=>u.Users)
                    .ThenInclude(u => u.User)
                .Include(gp => gp.GroupPhoto)
                .Include(g => g.SubCategories)
                    .ThenInclude(gc => gc.SubCategory)
                .SingleOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Group>> GetGroups()
        {
            return await _context.Groups
                .Include(gu=>gu.Users)
                    .ThenInclude(u=>u.User)
                .Include(gp => gp.GroupPhoto)
                .Include(g => g.SubCategories)
                    .ThenInclude(g => g.SubCategory)
                    .ThenInclude(gmc => gmc.MainCategory)
                .ToListAsync();
        }

        public async Task AddUserGroupAsync(UserGroup userGroup)
        {
            await _context.Set<UserGroup>().AddAsync(userGroup);
        }

        public async Task SaveUserGroupAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IfBelongToGroup(int groupId, int userId)
        {
            return await _context.UserGroups.AnyAsync(a => a.GroupId == groupId & a.UserId == userId);
        }
    }
}