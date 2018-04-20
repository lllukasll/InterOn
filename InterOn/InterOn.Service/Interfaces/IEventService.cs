using System.Threading.Tasks;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto.Event;

namespace InterOn.Service.Interfaces
{
    public interface IEventService
    {
        Task<bool> IfUserBelongToGroup(int eventId, int userId);
        Task<bool> ExistEvent(int id);
        Task<bool> ExistGroup(int id);
        Task<CreateEventDto> CreateEventAsync(CreateEventDto eventDto);
        Task<CreateEventDto> CreateEventForGroupAsync(CreateEventDto eventDto, int groupId);
        Task<UpdateEventDto> UpdateEventAsync(int id, UpdateEventDto eventDto);
        Task<UpdateEventDto> CreateEventUserAsync(int eventId, int userId);
    }
}