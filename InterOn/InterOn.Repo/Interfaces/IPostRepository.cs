using System.Threading.Tasks;
using InterOn.Data.DbModels;

namespace InterOn.Repo.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<bool> IfUserAddPost(int postId, int userId);
        Task<Post> GetPostGroupAsync(int postId);
        Task<bool> IfGroupExist(int groupId);
    }
}