using System;
using System.Threading.Tasks;
using AutoMapper;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto.Message;
using InterOn.Repo.Interfaces;
using InterOn.Service.Interfaces;

namespace InterOn.Service.Services
{
    public class UserMessageService : IUserMessageService
    {
        private readonly IUserMessageRepository _repository;
        private readonly IMapper _mapper;

        public UserMessageService(IUserMessageRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task SendMessageAsync(int userIdLogged, int userId, SendMessageDto sendMessageDto)
        {
            var message = _mapper.Map<SendMessageDto, Message>(sendMessageDto);
            message.ReceiverId = userId;
            message.SenderId = userIdLogged;
            message.CreateDateTime = DateTime.Now;
            await _repository.AddAsyn(message);
            await _repository.SaveAsync();
        }
    }
}