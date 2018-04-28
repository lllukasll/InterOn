using System.Threading.Tasks;
using InterOn.Data.DbModels;

namespace InterOn.Repo.Interfaces
{
    public interface IUserFriendRepository : IRepository<Friend>
    {
        Task<Friend> GetConfirmFriendAsync(int userIdLog, int userId);
    }
}