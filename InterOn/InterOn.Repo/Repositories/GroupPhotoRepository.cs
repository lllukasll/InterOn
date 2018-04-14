using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterOn.Data.DbModels;
using InterOn.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InterOn.Repo.Repositories
{
    public class GroupPhotoRepository:Repository<GroupPhoto>,IGroupPhotoRepository
    {
        public GroupPhotoRepository(DataContext context) : base(context)
        {
        }

        public Task<List<GroupPhoto>> GetGroupPhoto(int id)
        {
            return _context.GroupPhotos.Where(p => p.GroupRef == id).ToListAsync();
        }
    }
}