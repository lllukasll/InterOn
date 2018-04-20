using System;
using System.Threading.Tasks;
using InterOn.Data.ModelsDto.Group;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterOn.Api.Controllers
{

    [Authorize]
    [Route("/api/group")]
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupRepository)
        {
            _groupService = groupRepository;         
        }
        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupDto groupDto)
        {
            try
            {

                if (!ModelState.IsValid) return BadRequest(ModelState);
                var userId = int.Parse(HttpContext.User.Identity.Name);
                var result = await _groupService.CreateGroup(groupDto, userId);
                return Ok(result);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroup(int id,[FromBody] UpdateGroupDto groupDto)
        {
            if (await _groupService.IfExist(id) == false)
                return NotFound();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _groupService.UpdateGroup(groupDto, id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroup(int id)
        {
            try
            {

                var group = await _groupService.GetGroupMappedAsync(id);
                if (group == null) return NotFound();

                return Ok(group);

            }
            catch (Exception e)
            {
                return BadRequest($"{e.Message}");
            }

            
        }

        [HttpGet]
        public async Task<IActionResult> GetGroups()
        {
            var group = await _groupService.GetGroupsAsync();
            if (group == null) return NotFound();
            return Ok(group);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            if (await _groupService.IfExist(id) == false)
                return NotFound();

            _groupService.Remove(id);

            return Ok(id);
        }
        [HttpPost("{groupId}")]
        public async Task<IActionResult> AddUserForGroup(int groupId)
        {
            var userId = int.Parse(HttpContext.User.Identity.Name);
            if (await _groupService.IfUserBelongToGroupAsync(userId, groupId))
                return BadRequest("Już użytkownik należy do grupy");
            
            var result = await _groupService.CreateUserGroup(groupId, userId);

            return Ok(result);

        }
    }
}