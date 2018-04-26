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
    }
}