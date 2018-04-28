using System.Linq;
using System.Threading.Tasks;
using InterOn.Data.DbModels;
using InterOn.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InterOn.Repo.Repositories
{
    public class UserFriendRepository : Repository<Friend>, IUserFriendRepository
    {
        public UserFriendRepository(DataContext context) : base(context)
        {
        }

        public async Task<Friend> GetConfirmFriendAsync(int userIdLog, int userId) => await _context.Friends.Where(a => a.UserAId == userIdLog |a.UserBId==userIdLog & a.UserAId==userId | a.UserBId == userId).SingleAsync();
    }
}