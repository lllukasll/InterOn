using System.Threading.Tasks;
using InterOn.Data.DbModels;

namespace InterOn.Repo.Interfaces
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<bool> IfGroupExist(int id);
    }
}