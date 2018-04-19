﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto.Group;
using InterOn.Repo.Interfaces;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace InterOn.Service.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _repository;
        private readonly IMapper _mapper;

        public GroupService(IGroupRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Group> GetGroupAsync(int id, bool includeRelated = true)
        {
            if (!includeRelated) return await _repository.GetGroup(id, false);
            return await _repository.GetGroup(id);
        }

        public async Task<GroupDto> GetGroupMappedAsync(int id)
        {
            var group = await GetGroupAsync(id);
            var resultMap = _mapper.Map<Group, GroupDto>(group);
            return resultMap;
        }

        public async Task<IEnumerable<GroupDto>> GetGroupsAsync()
        {
            var group = await _repository.GetGroups();
            var result = _mapper.Map<IEnumerable<Group>, IEnumerable<GroupDto>>(group);
            return result;
        }

        public async void Remove(int id)
        {
            var group = await _repository.GetGroup(id, includeRelated: false);
            _repository.Remove(group);
            await _repository.SaveAsync();
        }
 
        public async Task<Group> CreateGroup(CreateGroupDto groupDto,int userId)
        {
            
            var group = _mapper.Map<CreateGroupDto, Group>(groupDto);
            group.CreateDateTime = DateTime.Now;
            group.UserId = userId;
            await _repository.AddAsyn(group);
            await _repository.SaveAsync();
            
            return group;
        }

        public async Task<UpdateGroupDto> UpdateGroup(UpdateGroupDto groupDto, int id)
        {
            var group = await _repository.GetGroup(id);
            _mapper.Map(groupDto, group);
            await _repository.SaveAsync();
            var groupResult = await _repository.GetGroup(group.Id);
            var result = _mapper.Map<Group, UpdateGroupDto>(groupResult);
            return result;
        }

        public async Task<bool> IfExist(int id)
        {
            return await _repository.Exist(g => g.Id == id);
        }
    }
}