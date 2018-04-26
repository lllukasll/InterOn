using System.Threading.Tasks;
using AutoMapper;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto.Event;
using InterOn.Repo.Interfaces;
using InterOn.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InterOn.Service.Services
{
    public class EventService : IEventService
    {
        private readonly IMapper _mapper;
        private readonly IEventRepository _repository;

        public EventService(IMapper mapper,IEventRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task CreateEventAsync(int userId, CreateEventDto eventDto)
        {
            var eventt = _mapper.Map<CreateEventDto, Event>(eventDto);
            eventt.UserId = userId;
            await _repository.AddAsyn(eventt);
            await _repository.SaveAsync();
        }

        public async Task<UpdateEventDto> UpdateEventAsync(int id, UpdateEventDto eventDto)
        {
            var eventt = await _repository.GetAllIncluding(a => a.SubCategories).SingleOrDefaultAsync(a => a.Id == id);
            _mapper.Map(eventDto, eventt);
            await _repository.SaveAsync();
            eventt = await _repository.GetAsync(eventt.Id);
            var result = _mapper.Map<Event,UpdateEventDto>(eventt);
            return result;
        }

        public async Task<UpdateEventDto> CreateEventUserAsync(int eventId, int userId)
        {
            var userEvent = new UserEvent
            {
                UserId = userId,
                EventId = eventId
            };

            await _repository.AddUserEvent(userEvent);
            await _repository.SaveAsync();
            var eventt = await _repository.GetAsync(eventId);
            var result = _mapper.Map<Event, UpdateEventDto>(eventt);
            return result;
        }

        public async Task CreateEventForGroupAsync(CreateEventDto eventDto, int groupId, int userId)
        {
            var eventforgroup = _mapper.Map<CreateEventDto, Event>(eventDto);
            eventforgroup.GroupId = groupId;
            eventforgroup.UserId = userId;
            await _repository.AddAsyn(eventforgroup);
            await _repository.SaveAsync();

        }

        public async Task RemoveUserEvent(int userId, int eventId)
        {
            var userEvent = await _repository.GetUserEvent(userId, eventId);
            _repository.RemoveUserEvent(userEvent);
            await _repository.SaveAsync();
        }

        public async Task<bool> ExistEvent(int id) => await _repository.Exist(e => e.Id == id);

        public async Task<bool> ExistGroup(int id) => await _repository.IfGroupExist(id);

        public async Task<bool> IfUserBelongToEvent(int eventId, int userId) => await _repository.IfBelongToEventAsync(eventId, userId);
        
        public async Task<bool> IsAdminEvent(int eventId, int userId) => await _repository.IsAdmin(userId, eventId);
    }
}