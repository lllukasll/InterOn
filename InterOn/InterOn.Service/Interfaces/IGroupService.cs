using System.Collections.Generic;
using System.Threading.Tasks;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto.Group;

namespace InterOn.Service.Interfaces
{
    public interface IGroupService
    {
        Task RemoveUserGroup(int userId, int groupId);
        Task<bool> IfUserBelongToGroupAsync(int userId, int groupId);
        Task<GroupDto> CreateUserGroup(int groupId, int userId);
        Task<GroupDto> GetGroupMappedAsync(int id);
        Task<int> CreateGroup(CreateGroupDto groupDto, int userId);
        Task<Group> GetGroupAsync(int id, bool includeRelated = true);
        Task<IEnumerable<GroupDto>> GetGroupsAsync();
        Task Remove(int id);
        Task<UpdateGroupDto> UpdateGroup(UpdateGroupDto groupDto, int id);
        Task<bool> IfExist(int id);
    }
}