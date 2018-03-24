using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using InterOn.Data.DbModels;

namespace InterOn.Repo.Interfaces
{
    public interface IGroupRepository
    {
        
        void AddAsync(Group group);
        Task<Group> GetGroup(int id, bool includeRelated = true);
        void Remove(Group group);
        Task<IEnumerable<Group>> GetGroups();
        
    }
}