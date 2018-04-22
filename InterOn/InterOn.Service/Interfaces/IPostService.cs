using System.Collections.Generic;
using System.Threading.Tasks;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto.Post;

namespace InterOn.Service.Interfaces
{
    public interface IPostService
    {
        Task<PostGroupDto> MapPostDto(int postId);
        Task<bool> IfExistPost(int postId);
        Task<bool> IfExistGroup(int groupId);
        Task<PostGroupDto> CreatePostGroupAsync(int groupId, int userId, CreateGroupPostDto createGroupPostDto);
        Task<Post> UpdatePostGroupAsync(int groupId, int postId, UpdateGroupPostDto updateGroupPost);
        Task RemovePost(int postId);
        Task<IEnumerable<PostGroupDto>> GetAllPostsForGroupAsync(int groupId);
    }
}