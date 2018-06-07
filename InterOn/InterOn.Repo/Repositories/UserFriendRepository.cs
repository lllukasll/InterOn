using System.Collections.Generic;
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

        public async Task<bool> IsFriendshipExist(int userIdLog, int userId) => 
            await _context.Friends.AnyAsync(a =>
                (a.UserAId == userIdLog | a.UserBId == userIdLog) & (a.UserAId == userId | a.UserBId == userId));
        public async Task<Friend> GetConfirmFriendAsync(int userIdLog, int userId) => 
            await _context.Friends
                .Where(a => a.UserBId == userIdLog & a.UserAId==userId)
                .SingleAsync();
        public async Task<IEnumerable<Friend>> GetConfirmedFriendsAsyn(int userId)
        {
            return await _context.Friends
                .Include(a=>a.UserA)
                .Include(a=>a.UserB)
                .Where(a => a.Confirmed && ( a.UserAId == userId || a.UserBId==userId && a.UserAId != a.UserBId ))
                .ToListAsync();
        }

        public async Task<bool> IsUserExist(int userId)
        {
            return await _context.Users.AnyAsync(a => a.Id == userId);
        } 

        public async Task<IEnumerable<Friend>> GetInvitationedFriendsAsyn(int userId)
        {
            return await _context.Friends
                .Include(a => a.UserA)
                .Include(a => a.UserB)
                .Where( a => a.Confirmed == false & a.UserBId == userId)
                .OrderBy(a=>a.Established)
                .ToListAsync();
        }
    }
}