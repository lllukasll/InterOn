using System.Collections.Generic;
using System.Threading.Tasks;
using InterOn.Data.ModelsDto.Post;

namespace InterOn.Service.Interfaces
{
    public interface IPostService
    {
        Task<bool> IfUserAddPostAsync(int postId, int userId);
        Task<PostGroupDto> MapPostDto(int postId);
        Task<bool> IfExistPost(int postId);
        Task<bool> IfExistGroupAsync(int groupId);
        Task<bool> IfExistEventAsync(int eventId);
        Task<PostGroupDto> CreatePostGroupAsync(int userId, CreateGroupPostDto createGroupPostDto);
        Task UpdatePostGroupAsync(int postId, UpdateGroupPostDto updateGroupPost);
        Task RemovePost(int postId);
        Task<IEnumerable<PostGroupDto>> GetAllPostsForGroupAsync(int groupId);
        Task<IEnumerable<PostGroupDto>> GetAllPostsForEventAsync(int eventId);
    }
}