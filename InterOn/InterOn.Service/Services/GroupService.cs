using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto.Group;
using InterOn.Repo.Interfaces;
using InterOn.Service.Interfaces;
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
            if (!includeRelated) return await _repository.GetGroupAsync(id, false);
            return await _repository.GetGroupAsync(id);
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

        public async Task<IEnumerable<GroupDto>> GetGroupsForUserAsync(int id)
        {
            var group = await _repository.GetGroupsForUser(id);
            var result = _mapper.Map<IEnumerable<Group>, IEnumerable<GroupDto>>(group);
            return result;
        }

        public async Task Remove(int id)
        {
            var group = await _repository.GetGroupAsync(id, false);
            _repository.Remove(group);
            await _repository.SaveAsync();
        }
 
        public async Task<int> CreateGroup(CreateGroupDto groupDto, int userId)
        {
            
            var group = _mapper.Map<CreateGroupDto, Group>(groupDto);
            group.CreateDateTime = DateTime.Now;
            group.UserId = userId;
            await _repository.AddAsyn(group);
            await _repository.SaveAsync();
          
            return group.Id;
        }

        public async Task<UpdateGroupDto> UpdateGroup(UpdateGroupDto groupDto, int id)
        {
            var group = await _repository.GetGroupAsync(id);
            _mapper.Map(groupDto, group);
            await _repository.SaveAsync();
            var groupResult = await _repository.GetGroupAsync(group.Id);
            var result = _mapper.Map<Group, UpdateGroupDto>(groupResult);
            return result;
        }

        public async Task<bool> IfExist(int id)
        {
            return await _repository.Exist(g => g.Id == id);
        }

        public async Task UploadPhoto(int groupId, IFormFile file, string uploadsFolderPath)
        {
            if (!Directory.Exists(uploadsFolderPath)) Directory.CreateDirectory(uploadsFolderPath);
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var group = await _repository.GetAsync(groupId);
            var photo = new GroupPhotoDto {GroupPhoto = $"{fileName}"};
            _mapper.Map(photo, group);
            await _repository.SaveAsync();
        }

        public async Task<GroupUnauthorizedDto> GetGroupAllowAnonymousAsync(int id)
        {
            var group =await _repository.GetGroupAsync(id);
            var mapResult = _mapper.Map<Group, GroupUnauthorizedDto>(group);
            return mapResult;
        }

        public async Task CreateUserGroup(int groupId, int userId)
        {
            var userGroup = new UserGroup {GroupId = groupId, UserId = userId};
            await _repository.AddUserGroupAsync(userGroup);
            await _repository.SaveUserGroupAsync();  
        }

        public async Task RemoveUserGroup(int userId, int groupId)
        {
            var userGroup = await _repository.GetUserGroupAsync(groupId, userId);
            _repository.RemoveUserGroup(userGroup);
            await _repository.SaveAsync();
        }

        public async Task<bool> IfUserBelongToGroupAsync(int userId, int groupId)
        {
            return await _repository.IfBelongToGroup(groupId, userId);
        }
        public async Task<bool> IsAdminAsync(int userId, int groupId)
        {
            return await _repository.IsAdmin(groupId, userId);
        }
    }
}