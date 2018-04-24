using System.Collections.Generic;
using System.Threading.Tasks;
using InterOn.Data.DbModels;

namespace InterOn.Repo.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<bool> IfUserAddComment(int commentId, int userId);
        Task<Comment> GetCommentsForUser(int commentId, int userId);
        Task<IEnumerable<Comment>> GetCommentsForPostGroup(int postId);
        Task<bool> IfGroupExist(int groupId); 
        Task<bool> IfPostExist(int postId);
    }
}