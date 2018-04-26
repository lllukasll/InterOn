using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterOn.Api.Controllers
{
    [Authorize]
    [Route("/api/group/{groupId}/user")]
    public class GroupUserController : Controller
    {
        private readonly IGroupService _groupService;

        public GroupUserController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        [HttpPost]
        public async Task<IActionResult> AddUserForGroup(int groupId)
        {
            if (await _groupService.IfExist(groupId) == false)
                return NotFound();
            var userId = int.Parse(HttpContext.User.Identity.Name);
            if (await _groupService.IfUserBelongToGroupAsync(userId, groupId))
                return BadRequest("Już użytkownik należy do grupy");

           await _groupService.CreateUserGroup(groupId, userId);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveUserGroup(int groupId)
        {
            var userId = int.Parse(HttpContext.User.Identity.Name);
            if (await _groupService.IfUserBelongToGroupAsync(userId, groupId) == false)
                return BadRequest("Już użytkownik nie należy do grupy");

            await _groupService.RemoveUserGroup(userId, groupId);

            return Ok();
        }
    }
}