using System.Threading.Tasks;
using InterOn.Data.ModelsDto.Event;

namespace InterOn.Service.Interfaces
{
    public interface IEventService
    {
        Task RemoveUserEvent(int userId, int eventId);
        Task<bool> IfUserBelongToEvent(int eventId, int userId);
        Task<bool> ExistEvent(int id);
        Task<bool> ExistGroup(int id);
        Task<CreateEventDto> CreateEventAsync(CreateEventDto eventDto);
        Task<CreateEventDto> CreateEventForGroupAsync(CreateEventDto eventDto, int groupId);
        Task<UpdateEventDto> UpdateEventAsync(int id, UpdateEventDto eventDto);
        Task<UpdateEventDto> CreateEventUserAsync(int eventId, int userId);
    }
}