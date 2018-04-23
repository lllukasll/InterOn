using System;

namespace InterOn.Data.ModelsDto.Comments
{
    public class CreateGroupPostCommentDto
    {
        public string Content { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}
