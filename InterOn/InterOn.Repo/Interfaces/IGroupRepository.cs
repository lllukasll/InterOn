using System.Collections.Generic;
using System.Threading.Tasks;
using InterOn.Data.DbModels;

namespace InterOn.Repo.Interfaces
{
    public interface IGroupRepository : IRepository<Group>
    {
        Task<Group> GetGroup(int id, bool includeRelated = true);

        Task<IEnumerable<Group>> GetGroups();
    }
}