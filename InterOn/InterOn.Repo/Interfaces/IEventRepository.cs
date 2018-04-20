using System.Threading.Tasks;
using InterOn.Data.DbModels;

namespace InterOn.Repo.Interfaces
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<bool> IfBelongToEventAsync(int eventId, int userId);
        Task AddUserEvent(UserEvent userEvent);
        Task<bool> IfGroupExist(int id);
    }
}