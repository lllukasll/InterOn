using System.Threading.Tasks;
using InterOn.Data.ModelsDto.Group;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InterOn.Api.Controllers
{
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
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _groupService.CreateGroup(groupDto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroup(int id,[FromBody] UpdateGroupDto groupDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _groupService.UpdateGroup(groupDto, id);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroup(int id)
        {
            var group = await _groupService.GetGroupMappedAsync(id);
            if (group == null) return NotFound();
            return Ok(group);
        }
        [HttpGet]
        public async Task<IActionResult> GetGroups()
        {
            var group = await _groupService.GetGroupsAsync();
            if (group == null) return NotFound();
            return Ok(group);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGroup(int id)
        {
            if (_groupService.IfExist(id) == false)
                return NotFound();

            _groupService.Remove(id);

            return Ok(id);
        }
    }
}