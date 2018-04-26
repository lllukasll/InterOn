using System.Threading.Tasks;
using InterOn.Data.ModelsDto.Event;

namespace InterOn.Service.Interfaces
{
    public interface IEventService
    {
        Task<bool> IsAdminEvent(int eventId, int userId);
        Task RemoveUserEvent(int userId, int eventId);
        Task<bool> IfUserBelongToEvent(int eventId, int userId);
        Task<bool> ExistEvent(int id);
        Task<bool> ExistGroup(int id);
        Task CreateEventAsync(int userId,CreateEventDto eventDto);
        Task<UpdateEventDto> UpdateEventAsync(int id, UpdateEventDto eventDto);
        Task<UpdateEventDto> CreateEventUserAsync(int eventId, int userId);
        Task CreateEventForGroupAsync(CreateEventDto eventDto, int groupId, int userId);
    }
}