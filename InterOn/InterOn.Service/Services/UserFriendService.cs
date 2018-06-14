using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto.User;
using InterOn.Repo.Interfaces;
using InterOn.Service.Interfaces;

namespace InterOn.Service.Services
{
    public class UserFriendService : IUserFriendService
    {
        private readonly IUserFriendRepository _repository;
        private readonly IMapper _mapper;

        public UserFriendService(IUserFriendRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddFriend(int userIdLog, int userId)
        {
            var friend = new Friend
            {
                Confirmed = false,
                UserAId = userIdLog,
                UserBId = userId,
                Established = DateTime.Now,
                ConversationName = userIdLog.ToString() + userId.ToString()
            };
            await _repository.AddAsyn(friend);
            await _repository.SaveAsync();
        }

        public async Task ConfirmFriend(int userIdLog, int userId)
        {
            var friend = await _repository.GetConfirmFriendAsync(userIdLog, userId);
        
            var addconfirm = new ConfirmFriendDto
            {
                Confirmed = true
            };
            _mapper.Map(addconfirm, friend);
            await _repository.SaveAsync();
         
        }

        public async Task RemoveFriendAsync(int userIdLog, int userId)
        {
            //var friend = await _repository.GetConfirmFriendAsync(userIdLog, userId);
            var f = await _repository.GetAsync(userId);
            await _repository.DeleteAsyn(f);
        }

        public async Task<bool> IsExistFriendship(int userIdLog, int userId)
        {
            return await _repository.IsFriendshipExist(userIdLog, userId);
        }

        public async Task<bool> IsExistUser(int userId)
        {
            return await _repository.IsUserExist(userId);
        }
        public async Task<IEnumerable<FriendDto>> GetInvFriendsAsync(int userIdLogged)
        {
            var friends = await _repository.GetInvitationedFriendsAsyn(userIdLogged);
            var result = _mapper.Map<IEnumerable<Friend>, IEnumerable<FriendDto>>(friends);

            return result;
        }

        public async Task<IEnumerable<FriendDto>> GetConfirmedFriendsAsync(int userId)
        {
            var friends = await _repository.GetConfirmedFriendsAsyn(userId);
            var result = _mapper.Map<IEnumerable<Friend>, IEnumerable<FriendDto>>(friends);

            return result;
        }
    }
}