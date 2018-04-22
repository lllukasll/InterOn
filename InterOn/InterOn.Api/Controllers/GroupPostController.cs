using System.Threading.Tasks;
using InterOn.Data.ModelsDto.Post;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterOn.Api.Controllers
{
    [Authorize]
    [Route("/api/group/{groupId}/post")]
    public class GroupPostController : Controller
    {
        private readonly IPostService _service;

        public GroupPostController(IPostService service)
        {
            _service = service;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreatePostForGroup(int groupId,[FromBody] CreateGroupPostDto createGroupPostDto )
        {
            var userId = int.Parse(HttpContext.User.Identity.Name);
            if (await _service.IfExistGroup(groupId) == false)
                return NotFound("Nie ma takiej grupy");
            var post =  await _service.CreatePostGroupAsync(groupId, userId, createGroupPostDto);

            return Ok(post);
        }
        
        [HttpPut("{postId}")]
        public async Task<IActionResult> UpdatePostForGroup(int groupId,int postId, [FromBody] UpdateGroupPostDto updateGroupPost)
        {
            if (await _service.IfExistGroup(groupId) == false)
                return NotFound("Nie ma takiej grupy");
            if (await _service.IfExistPost(postId) == false)
                return NotFound("Nie ma takiego postu");
             await _service.UpdatePostGroupAsync(groupId,postId,updateGroupPost);
            var result = await _service.MapPostDto(postId);
            return Ok(result);
        }
        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeletePostForGroup(int postId,int groupId)
        {
            if (await _service.IfExistGroup(groupId) == false)
                return NotFound("Nie ma takiej grupy");
            if (await _service.IfExistPost(postId) == false)
                return NotFound("Nie ma takiego postu");
            await _service.RemovePost(postId);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetPostsForGroup(int groupId)
        {
            if (await _service.IfExistGroup(groupId) == false)
                return NotFound("Nie ma takiej grupy");
            var sss = await _service.GetAllPostsForGroupAsync(groupId);
            return Ok(sss);
        }
    }
}