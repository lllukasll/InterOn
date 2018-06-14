using System;
using System.Threading.Tasks;
using InterOn.Api.Helpers;
using InterOn.Data.ModelsDto.Post;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterOn.Api.Controllers
{
    [Authorize]
    [Route("/api/post")]
    public class GroupPostController : Controller
    {
        private readonly IPostService _service;

        public GroupPostController(IPostService service)
        {
            _service = service;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreatePostForGroup([FromBody] CreateGroupPostDto createGroupPostDto )
        {
            if (!ModelState.IsValid)
                return new UnprocessableEntityObjectResult(ModelState);
            var userId = int.Parse(HttpContext.User.Identity.Name);
            //if (await _service.IfExistGroupAsync(groupId) == false)
            //    return NotFound("Nie ma takiej grupy");
            var post =  await _service.CreatePostGroupAsync(userId, createGroupPostDto);

            return Ok(post);
        }

        [HttpPut("{postId}")]
        public async Task<IActionResult> UpdatePostForGroup(int groupId, int postId, [FromBody] UpdateGroupPostDto updateGroupPost)
        {
            if (!ModelState.IsValid)
                return new UnprocessableEntityObjectResult(ModelState);
            var userId = int.Parse(HttpContext.User.Identity.Name);
            //if (await _service.IfExistGroupAsync(groupId) == false)
            //    return NotFound("Nie ma takiej grupy");
            if (await _service.IfExistPost(postId) == false)
                return NotFound("Nie ma takiego postu");
            if (await _service.IfUserAddPostAsync(postId, userId) == false)
                return BadRequest("Użytkownik nie może edytować posta");
            await _service.UpdatePostGroupAsync(postId,updateGroupPost);
            
            var result = await _service.MapPostDto(postId);
            return Ok(result);
        }
        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeletePostForGroup(int postId)
        {
            var userId = int.Parse(HttpContext.User.Identity.Name);
            //if (await _service.IfExistGroupAsync(groupId) == false)
            //    return NotFound("Nie ma takiej grupy");
            if (await _service.IfExistPost(postId) == false)
                return NotFound("Nie ma takiego postu");
            if (await _service.IfUserAddPostAsync(postId, userId) == false)
                return BadRequest("Użytkownik nie może usunąć posta");
            await _service.RemovePost(postId);
             return Ok();
        }

        [HttpGet("group/{groupId}")]
        public async Task<IActionResult> GetPostsForGroup(int groupId)
        {
            try
            {
                if (await _service.IfExistGroupAsync(groupId) == false)
                    return NotFound("Nie ma takiej grupy");
                var resultDtos = await _service.GetAllPostsForGroupAsync(groupId);
                return Ok(resultDtos);
            }
            catch (Exception e)
            {
               return BadRequest(e.InnerException);
            }
        }

        [HttpGet("event/{eventId}")]
        public async Task<IActionResult> GetPostsForEvent(int eventId)
        {
            try
            {
                if (await _service.IfExistEventAsync(eventId) == false)
                    return NotFound("Nie ma takiej grupy");
                var resultDtos = await _service.GetAllPostsForEventAsync(eventId);
                return Ok(resultDtos);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException);
            }
        }
    }
}