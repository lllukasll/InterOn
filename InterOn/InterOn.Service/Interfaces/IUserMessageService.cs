using System.Threading.Tasks;
using InterOn.Data.ModelsDto.Message;

namespace InterOn.Service.Interfaces
{
    public interface IUserMessageService
    {
        Task SendMessageAsync(int userIdLogged, int userId, SendMessageDto sendMessageDto);
    }
}