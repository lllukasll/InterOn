using System.Collections.Generic;
using System.Threading.Tasks;
using InterOn.Data.DbModels;
using InterOn.Repo.Interfaces;

namespace InterOn.Service.Interfaces
{
    public interface IGroupService
    {
        Task<Group> GetGroupAsync(int id, bool includeRelated = true);
        Task<IEnumerable<Group>> GetGroupsAsync();
        Task AddAsync(Group group);
        void Remove(Group group);
    }
}