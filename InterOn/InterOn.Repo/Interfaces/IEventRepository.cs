using System.Collections.Generic;
using System.Threading.Tasks;
using InterOn.Data.DbModels;

namespace InterOn.Repo.Interfaces
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<bool> IsAdmin(int userId, int eventId);
        Task<UserEvent> GetUserEvent(int userId, int eventId);
        Task<bool> IfBelongToEventAsync(int eventId, int userId);
        Task AddUserEvent(UserEvent userEvent);
        Task<bool> IfGroupExist(int id);
        void RemoveUserEvent(UserEvent userEvent);
        Task<IEnumerable<Event>> GetGroupEvents(int groupId);
        Task<IEnumerable<Event>> GetEvents();
        Task<Event> GetEventAsync(int eventId,int groupId);
    }
}