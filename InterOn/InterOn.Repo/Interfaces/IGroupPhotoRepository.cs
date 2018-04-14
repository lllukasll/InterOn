using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterOn.Data.DbModels;

namespace InterOn.Repo.Interfaces
{
    public interface IGroupPhotoRepository : IRepository<GroupPhoto>
    {
        Task<List<GroupPhoto>> GetGroupPhoto(int id);
    }
}