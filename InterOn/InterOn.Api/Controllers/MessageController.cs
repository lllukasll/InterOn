using System.Threading.Tasks;
using InterOn.Data.ModelsDto.Message;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InterOn.Api.Controllers
{
    [Route("/api/messages/{userId}")]
    public class MessageController : Controller
    {
        private readonly IUserMessageService _service;

        public MessageController(IUserMessageService service)
        {
            _service = service;
        }
        [HttpPost("send")]
        public async Task<IActionResult> Send(int userId,[FromBody] SendMessageDto sendMessageDto)
        {
            var userIdLogged = int.Parse(HttpContext.User.Identity.Name);
            await _service.SendMessageAsync(userIdLogged, userId, sendMessageDto);

            return Ok();
        }

    }
}