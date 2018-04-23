using System.Threading.Tasks;
using InterOn.Data.ModelsDto.Comments;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterOn.Api.Controllers
{
    [Authorize]
    [Route("/api/group/{groupId}/post/{postId}/comment")]
    public class GroupPostCommentsController : Controller
    {
        private readonly ICommentService _service;

        public GroupPostCommentsController(ICommentService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCommentsForPostGroup(int groupId, int postId,
            [FromBody] CreateGroupPostCommentDto commentsDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userId = int.Parse(HttpContext.User.Identity.Name);
            if (await _service.IfGroupExistAsync(groupId) == false)
                return NotFound("Nie ma takiej Grupy");
            if (await _service.IfPostExistAsync(postId) == false)
                return NotFound("Nie ma Takiego Postu");
            await _service.CreateCommentForGroupAsync(groupId, userId, postId, commentsDto);
            return StatusCode(201);
        }

        [HttpPut("{commentId}")]
        public async Task<IActionResult> UpdateCommentsForPostGroup(int groupId, int postId, int commentId,
            [FromBody] UpdateGroupPostCommentDto commentsDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (await _service.IfGroupExistAsync(groupId) == false)
                return NotFound("Nie ma takiej Grupy");
            if (await _service.IfPostExistAsync(postId) == false)
                return NotFound("Nie ma Takiego Postu");
            if (await _service.IfCommentExistAsync(commentId) == false)
                return NotFound("Nie ma Takiego Komentarza");
            await _service.UpdateCommentForGroupAsync(commentId, commentsDto);
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCommentsForPostGroup(int postId, int groupId)
        {
            if (await _service.IfGroupExistAsync(groupId) == false)
                return NotFound("Nie ma takiej Grupy");
            if (await _service.IfPostExistAsync(postId) == false)
                return NotFound("Nie ma Takiego Postu");
            var result = await _service.GetAllCommentsFromPostGroup(postId);
            return Ok(result);
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteCommentForPostGroup(int commentId, int postId, int groupId)
        {
            if (await _service.IfGroupExistAsync(groupId) == false)
                return NotFound("Nie ma takiej Grupy");
            if (await _service.IfPostExistAsync(postId) == false)
                return NotFound("Nie ma Takiego Postu");
            if (await _service.IfCommentExistAsync(commentId) == false)
                return NotFound("Nie ma Takiego Komentarza");
            await _service.DeleteComment(commentId);

            return Ok();
        }
    }
}