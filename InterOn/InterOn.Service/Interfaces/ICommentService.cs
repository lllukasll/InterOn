using System.Collections.Generic;
using System.Threading.Tasks;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto.Comments;

namespace InterOn.Service.Interfaces
{
    public interface ICommentService
    {
        Task<bool> IfUserAddCommentAsync(int commentId, int userId);
        Task<bool> IfCommentExistAsync(int commentId);
        Task<IEnumerable<CommentDto>> GetAllCommentsFromPostGroup(int postId);
        Task CreateCommentForGroupAsync(int groupId, int userId, int postId,
            CreateGroupPostCommentDto createGroupComment);
        Task<bool> IfGroupExistAsync(int groupId);
        Task<bool> IfPostExistAsync(int postId);
        Task UpdateCommentForGroupAsync(int commentId, UpdateGroupPostCommentDto commentsDto);
        Task DeleteComment(int commentId);
    }
}