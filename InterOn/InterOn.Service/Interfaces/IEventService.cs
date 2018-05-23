using System.Collections.Generic;
using System.Threading.Tasks;
using InterOn.Data.ModelsDto.Event;
using Microsoft.AspNetCore.Http;

namespace InterOn.Service.Interfaces
{
    public interface IEventService
    {
        Task UploadPhoto(int eventId, IFormFile file, string uploadsFolderPath);
        Task<bool> IsAdminEvent(int eventId, int userId);
        Task RemoveUserEvent(int userId, int eventId);
        Task<bool> IfUserBelongToEvent(int eventId, int userId);
        Task<bool> ExistEvent(int id);
        Task<bool> ExistGroup(int id);
        Task<EventDto> CreateEventAsync(int userId, CreateEventDto eventDto);
        Task UpdateEventAsync(int eventId, UpdateEventDto eventDto);
        Task CreateEventUserAsync(int eventId, int userId);
        Task<EventDto> CreateEventForGroupAsync(CreateEventDto eventDto, int groupId, int userId);
        Task<IEnumerable<EventDto>> GetAllEventGroupAsync(int groupId);
        Task Delete(int eventId);
        Task<IEnumerable<EventDto>> GetAllEventAsync();
        Task<EventDto> GetEventAsync(int eventId);
        Task<EventDto> GetEventAsync(int eventId, int groupId);
    }
}