using System.Collections.Generic;
using System.Threading.Tasks;
using InterOn.Data.DbModels;

namespace InterOn.Repo.Interfaces
{
    public interface IGroupRepository : IRepository<Group>
    {
        Task<bool> IsAdmin(int groupId, int userId);
        Task<UserGroup> GetUserGroupAsync(int groupId, int userId);
        Task<bool> IfBelongToGroup(int groupId, int userId);
        Task SaveUserGroupAsync();
        Task AddUserGroupAsync(UserGroup userGroup);
        void RemoveUserGroup(UserGroup userGroup);
        Task<Group> GetGroupAsync(int id, bool includeRelated = true);
        Task<IEnumerable<Group>> GetGroups();
        Task<IEnumerable<Group>> GetGroupsForUser(int id);
    }
}