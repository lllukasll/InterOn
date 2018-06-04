using System.Collections.Generic;
using System.Threading.Tasks;
using InterOn.Data.DbModels;

namespace InterOn.Repo.Interfaces
{
    public interface IUserFriendRepository : IRepository<Friend>
    {
        Task<bool> IsFriendshipExist(int userIdLog, int userId);
        Task<bool> IsUserExist(int userId);
        Task<Friend> GetConfirmFriendAsync(int userIdLog, int userId);
        Task<IEnumerable<Friend>> GetConfirmedFriendsAsyn(int userId);
        Task<IEnumerable<Friend>> GetInvitationedFriendsAsyn(int userId);
    }
}