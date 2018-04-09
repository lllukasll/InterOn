﻿using System.Collections.Generic;
using System.Threading.Tasks;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto.Group;
using InterOn.Repo.Interfaces;

namespace InterOn.Service.Interfaces
{
    public interface IGroupService
    {
        Task<CreateGroupDto> GetGroupMappedAsync(int id);
        Task<CreateGroupDto> CreateGroup(CreateGroupDto groupDto);
        Task<Group> GetGroupAsync(int id, bool includeRelated = true);
        Task<IEnumerable<Group>> GetGroupsAsync();
        Task AddAsync(Group group);
        void Remove(int id);
        Task<UpdateGroupDto> UpdateGroup(UpdateGroupDto groupDto, int id);
        bool IfExist(int id);
    }
}