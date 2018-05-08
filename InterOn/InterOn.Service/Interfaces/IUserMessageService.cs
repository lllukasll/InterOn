using System.Collections.Generic;
using System.Threading.Tasks;
using InterOn.Data.ModelsDto.Message;

namespace InterOn.Service.Interfaces
{
    public interface IUserMessageService
    {
        Task<IEnumerable<MessageDto>> GetMessagesAsync(int senderId, int receiverId);
        Task SendMessageAsync(int userIdLogged, int userId, SendMessageDto sendMessageDto);
    }
}