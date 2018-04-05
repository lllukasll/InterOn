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
    public class GroupService : IGroupService
    {
       
        private readonly IGroupRepository _context;

        public GroupService(IGroupRepository repository)
        {
            _context = repository;
        }
        public async Task<Group> GetGroupAsync(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await _context.GetGroup(id);

            return await _context.GetGroup(id);
        }

        public async Task<IEnumerable<Group>> GetGroupsAsync()
        {
            return await _context.GetGroups();
        }

        public async Task AddAsync(Group group)
        {
            await _context.AddAsyn(group);
        }

        public void Remove(Group group)
        {
            _context.Remove(group);
        }
    }
}