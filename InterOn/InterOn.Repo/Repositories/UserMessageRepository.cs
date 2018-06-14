using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterOn.Data.DbModels;
using InterOn.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InterOn.Repo.Repositories
{
    public class UserMessageRepository : Repository<Message>, IUserMessageRepository
    {
        public UserMessageRepository(DataContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Message>> GetMessageAsync(int senderId, int receiverId)
        {
            return await _context.Messages
                .Include(u => u.ReceiverUser)
                .Include(u => u.SenderUser)
                .OrderBy(o => o.CreateDateTime)
                .Where(a => (a.SenderId == senderId && a.ReceiverId == receiverId) || (a.ReceiverId == senderId && a.SenderId == receiverId)).ToListAsync();
        }
    }
}