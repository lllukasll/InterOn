using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using InterOn.Data.DbModels;
using InterOn.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InterOn.Repo.Repositories
{
    public class EventRepository : Repository<Event>,IEventRepository
    {
        public EventRepository(DataContext context) : base(context)
        {
        }

        public async Task<bool> IfGroupExist(int id)
        {
            return await _context.Set<Group>().AnyAsync(g => g.Id == id);
        }
    }
}