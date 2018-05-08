using System.Collections.Generic;
using System.Threading.Tasks;
using InterOn.Data.DbModels;

namespace InterOn.Repo.Interfaces
{
    public interface IUserMessageRepository : IRepository<Message>
    {
        Task<IEnumerable<Message>> GetMessageAsync(int senderId, int receiverId);
    }
}