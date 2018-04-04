using System.Collections.Generic;
using System.Threading.Tasks;
using InterOn.Data.DbModels;
using InterOn.Repo.Interfaces;

namespace InterOn.Service.Interfaces
{
    public interface IGroupService :IRepository<Group>
    {
        Task<Group> GetGroup(int id, bool includeRelated = true);

        Task<IEnumerable<Group>> GetGroups();
    }
}