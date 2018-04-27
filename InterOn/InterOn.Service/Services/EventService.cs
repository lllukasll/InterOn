using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto.Event;
using InterOn.Repo.Interfaces;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Http;
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

        public async Task UpdateEventAsync(int eventId, UpdateEventDto eventDto)
        {
            var eventt = await _repository.GetAllIncluding(a => a.SubCategories).SingleOrDefaultAsync(a => a.Id == eventId);
            _mapper.Map(eventDto, eventt);
            await _repository.SaveAsync();
        }

        public async Task CreateEventUserAsync(int eventId, int userId)
        {
            var userEvent = new UserEvent
            {
                UserId = userId,
                EventId = eventId
            };

            await _repository.AddUserEvent(userEvent);
            await _repository.SaveAsync();
        }

        public async Task CreateEventForGroupAsync(CreateEventDto eventDto, int groupId, int userId)
        {
            var eventforgroup = _mapper.Map<CreateEventDto, Event>(eventDto);
            eventforgroup.GroupId = groupId;
            eventforgroup.UserId = userId;
            await _repository.AddAsyn(eventforgroup);
            await _repository.SaveAsync();

        }

        public async Task<IEnumerable<EventGroupDto>> GetAllEventGroupAsync(int groupId)
        {
            var eventGroup = await _repository.GetGroupEvents(groupId);
            var resultMap = _mapper.Map<IEnumerable<Event>, IEnumerable<EventGroupDto>>(eventGroup);
            return resultMap;
        }

        public async Task Delete(int eventId)
        {
            var eventt = await _repository.GetAsync(eventId);
             _repository.Remove(eventt);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<EventDto>> GetAllEventAsync()
        {
            var eventt = await _repository.GetEvents();
            var resultMap = _mapper.Map<IEnumerable<Event>, IEnumerable<EventDto>>(eventt);
            return resultMap;
        }

        public async Task<EventDto> GetEventAsync(int eventId)
        {
            var eventt = await _repository.GetEventAsync(eventId, 0);
            var resultMap = _mapper.Map<Event, EventDto>(eventt);
            return resultMap;
        }
        public async Task<EventGroupDto> GetEventAsync(int eventId,int groupId)
        {
            var eventt = await _repository.GetEventAsync(eventId, groupId);
            var resultMap = _mapper.Map<Event, EventGroupDto>(eventt);
            return resultMap;
        }

        public async Task RemoveUserEvent(int userId, int eventId)
        {
            var userEvent = await _repository.GetUserEvent(userId, eventId);
            _repository.RemoveUserEvent(userEvent);
            await _repository.SaveAsync();
        }
        public async Task UploadPhoto(int eventId, IFormFile file, string uploadsFolderPath)
        {
            if (!Directory.Exists(uploadsFolderPath)) Directory.CreateDirectory(uploadsFolderPath);
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var eventt = await _repository.GetAsync(eventId);
            var test = new Event
            {
                PhotoUrl = $"{fileName}"
            };
            _mapper.Map(test,eventt);    
            await _repository.SaveAsync();
        }
        
        public async Task<bool> ExistEvent(int id) => await _repository.Exist(e => e.Id == id);

        public async Task<bool> ExistGroup(int id) => await _repository.IfGroupExist(id);

        public async Task<bool> IfUserBelongToEvent(int eventId, int userId) => await _repository.IfBelongToEventAsync(eventId, userId);
        
        public async Task<bool> IsAdminEvent(int eventId, int userId) => await _repository.IsAdmin(userId, eventId);
    }
}