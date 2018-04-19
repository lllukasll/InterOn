using System.Collections.Generic;
using System.Threading.Tasks;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto.Group;

namespace InterOn.Service.Interfaces
{
    public interface IGroupService
    {
        Task<GroupDto> GetGroupMappedAsync(int id);
        Task<GroupDto> CreateGroup(CreateGroupDto groupDto, int userId);
        Task<Group> GetGroupAsync(int id, bool includeRelated = true);
        Task<IEnumerable<GroupDto>> GetGroupsAsync();
        void Remove(int id);
        Task<UpdateGroupDto> UpdateGroup(UpdateGroupDto groupDto, int id);
        Task<bool> IfExist(int id);
    }
}