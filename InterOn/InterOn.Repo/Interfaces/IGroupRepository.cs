﻿using System.Collections.Generic;
using System.Threading.Tasks;
using InterOn.Data.DbModels;

namespace InterOn.Repo.Interfaces
{
    public interface IGroupRepository : IRepository<Group>
    {
        Task<UserGroup> GetUserGroupAsync(int groupId, int userId);
        Task<bool> IfBelongToGroup(int groupId, int userId);
        Task SaveUserGroupAsync();
        Task AddUserGroupAsync(UserGroup userGroup);
        void RemoveUserGroup(UserGroup userGroup);
        Task<Group> GetGroup(int id, bool includeRelated = true);
        Task<IEnumerable<Group>> GetGroups();
    }
}