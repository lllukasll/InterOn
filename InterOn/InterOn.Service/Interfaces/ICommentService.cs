using System.Collections.Generic;
using System.Threading.Tasks;
using InterOn.Data.ModelsDto.Comments;

namespace InterOn.Service.Interfaces
{
    public interface ICommentService
    {
        Task<bool> IfUserAddCommentAsync(int commentId, int userId);
        Task<bool> IfCommentExistAsync(int commentId);
        Task<IEnumerable<CommentDto>> GetAllCommentsForPostGroup(int postId);
        Task<CommentDto> CreateCommentForGroupAsync(int groupId, int userId, int postId,
            CreateGroupPostCommentDto createGroupComment);
        Task<bool> IfGroupExistAsync(int groupId);
        Task<bool> IfPostExistAsync(int postId);
        Task UpdateCommentForGroupAsync(int commentId, UpdateGroupPostCommentDto commentsDto);
        Task DeleteComment(int commentId);
        Task<CommentDto> GetCommentPostGroupAsync(int postId, int commentId);
    }
}