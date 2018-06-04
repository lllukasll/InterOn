using System.Collections.Generic;
using System.Threading.Tasks;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto.User;

namespace InterOn.Service.Interfaces
{
    public interface IUserFriendService
    {
        Task<bool> IsExistFriendship(int userIdLog, int userId);
        Task<bool> IsExistUser(int userId);
        Task<IEnumerable<FriendDto>> GetConfirmedFriendsAsync(int userId);
        Task AddFriend(int userIdLog, int userId);
        Task ConfirmFriend(int userIdLog, int userId);
        Task RemoveFriendAsync(int userIdLog, int userId);
        Task<IEnumerable<FriendDto>> GetInvFriendsAsync(int userIdLogged);
    }
}