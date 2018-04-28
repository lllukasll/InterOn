using System;
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
            var friend = await _repository.GetConfirmFriendAsync(userIdLog, userId);
            _repository.Remove(friend);
            await _repository.SaveAsync();
        }
    }
}