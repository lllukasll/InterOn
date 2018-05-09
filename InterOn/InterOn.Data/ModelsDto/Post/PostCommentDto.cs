using System;

namespace InterOn.Data.ModelsDto.Post
{
    public class PostCommentDto
    {
        public string Content { get; set; }

        public DateTime CreateDateTime { get; set; }

        public DateTime UpdateDateTime { get; set; }
        public UserDto User { get; set; }
    }
}