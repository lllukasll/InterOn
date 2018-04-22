using System.Threading.Tasks;
using InterOn.Data.DbModels;

namespace InterOn.Repo.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<Post> GetPostGroup(int groupId, int postId);
        Task<bool> IfGroupExist(int groupId);
    }
}