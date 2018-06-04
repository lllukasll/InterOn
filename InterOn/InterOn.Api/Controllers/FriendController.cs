using System.Threading.Tasks;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterOn.Api.Controllers
{
    [Authorize]
    [Route("/api/user/{userId}/friends")]
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
            if (!await _service.IsExistUser(userId))
            {
                return BadRequest("Nie ma takiego użytkowika o tym Id");
            }

            if (await _service.IsExistFriendship(userIdLogged, userId))
            {
                return BadRequest("Już dodałeś tą osobę do znajomych");
            }
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
            if (!await _service.IsExistUser(userId))
            {
                return BadRequest("Nie ma takiego użytkowika o tym Id");
            }
            if (userIdLogged == userId)
            {
                return BadRequest("Ten sam użytkownik nie może potwierdzić znajomych ");
            }

            if (!await _service.IsExistFriendship(userIdLogged, userId))
            {
                return BadRequest("Nie ma takiej znajomości do zatwierdzienia");
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
            if (!await _service.IsExistUser(userId))
            {
                return BadRequest("Nie ma takiego użytkowika o tym Id");
            }
            if (!await _service.IsExistFriendship(userIdLogged, userId))
            {
                return BadRequest("Nie ma takiech znajomych");
            }
             await _service.RemoveFriendAsync(userIdLogged, userId);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetConfirmedFriends(int userId)
        {
            if (!await _service.IsExistUser(userId))
            {
                return BadRequest("Nie ma takiego użytkowika o tym Id");
            }
            var userIdLogged = int.Parse(HttpContext.User.Identity.Name);
            var res = await _service.GetConfirmedFriendsAsync(userIdLogged);
            return Ok(res);
        }
        [HttpGet("invitation")]
        public async Task<IActionResult> GetInvitaionFriends(int userId)
        {
            if (!await _service.IsExistUser(userId))
            {
                return BadRequest("Nie ma takiego użytkowika o tym Id");
            }
            var userIdLogged = int.Parse(HttpContext.User.Identity.Name);
            var res = await _service.GetInvFriendsAsync(userIdLogged);
            return Ok(res);
        }
    }
}