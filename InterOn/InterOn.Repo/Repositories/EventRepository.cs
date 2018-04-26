using System.Collections.Generic;
using InterOn.Data.DbModels;
using InterOn.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace InterOn.Repo.Repositories
{
    public class EventRepository : Repository<Event>,IEventRepository
    {
        public EventRepository(DataContext context) : base(context)
        {
        }

        public async Task<bool> IfGroupExist(int id) => 
            await _context.Set<Group>().AnyAsync(g => g.Id == id);

        public async Task AddUserEvent(UserEvent userEvent) => 
            await _context.UserEvents.AddAsync(userEvent);

        public async Task<bool> IfBelongToEventAsync(int eventId, int userId) =>
            await _context.UserEvents.AnyAsync(a => a.EventId == eventId & a.UserId == userId);

        public async Task<bool> IsAdmin(int userId, int eventId) =>
            await _context.Events.AnyAsync(e => e.UserId == userId & e.Id == eventId);

        public async Task<UserEvent> GetUserEvent(int userId, int eventId) =>
            await _context.UserEvents.Where(a => a.EventId == eventId & a.UserId == userId).SingleAsync();

        public void RemoveUserEvent(UserEvent userEvent) => _context.UserEvents.Remove(userEvent);
        public async Task<IEnumerable<Event>> GetGroupEvents(int groupId)
        {
            return await _context.Events
                .Include(gu => gu.Users)
                .ThenInclude(u => u.User)
                .Include(g => g.SubCategories)
                .ThenInclude(g => g.SubCategory)
                .OrderBy(a=>a.DateTimeEvent)
                .Where(a=>a.GroupId==groupId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Event>> GetEvents()
        {
            return await _context.Events
                .Include(gu => gu.Users)
                .ThenInclude(u => u.User)
                .Include(g => g.SubCategories)
                .ThenInclude(g => g.SubCategory)
                .OrderBy(a => a.DateTimeEvent)
                .Where(a=>a.GroupId==0)
                .ToListAsync();
        }
        public async Task<Event> GetEventAsync(int eventId,int groupId)
        {
            return await _context.Events
                .Include(gu => gu.Users)
                    .ThenInclude(u => u.User)
                .Include(g => g.SubCategories)
                    .ThenInclude(g => g.SubCategory)
                .Where(a => a.GroupId == groupId)
                .SingleOrDefaultAsync(a => a.Id == eventId);
        }
    }
}