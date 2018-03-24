using System.Threading.Tasks;
using InterOn.Repo.Interfaces;
using SQLitePCL;

namespace InterOn.Repo.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}