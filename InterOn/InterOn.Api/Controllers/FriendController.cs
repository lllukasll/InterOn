using System.Threading.Tasks;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InterOn.Api.Controllers
{   [Route("/api/user/{userId}/friends")]
    public class FriendController : Controller
    {
        private readonly IUserFriendService _service;

        public FriendController(IUserFriendService service)
        {
            _service = service;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddFriend(int userId)
        {
            var userIdLogged = int.Parse(HttpContext.User.Identity.Name);

            if (userIdLogged == userId)
            {
                return BadRequest("Ten sam użytkownik nie może dodać się do znajomych ");
            }
            await _service.AddFriend(userIdLogged, userId);

            return Ok();
        }
        [HttpPut("confirm")]
        public async Task<IActionResult> ConfirmFriend(int userId)
        {
            var userIdLogged = int.Parse(HttpContext.User.Identity.Name);

            if (userIdLogged == userId)
            {
                return BadRequest("Ten sam użytkownik nie może potwierdzić znajomych ");
            }

            await _service.ConfirmFriend(userIdLogged, userId);
            return Ok();
        }
        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFriend(int userId)
        {
            var userIdLogged = int.Parse(HttpContext.User.Identity.Name);
            if (userIdLogged == userId)
            {
                return BadRequest("Ten sam użytkownik nie może usunąć siebie ze znajomych");
            }

            await _service.RemoveFriendAsync(userIdLogged, userId);
            return Ok();
        }
    }
}