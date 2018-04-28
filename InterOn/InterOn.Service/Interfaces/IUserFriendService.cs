using System.Threading.Tasks;

namespace InterOn.Service.Interfaces
{
    public interface IUserFriendService
    {
        Task AddFriend(int userIdLog, int userId);
        Task ConfirmFriend(int userIdLog, int userId);
        Task RemoveFriendAsync(int userIdLog, int userId);
    }
}