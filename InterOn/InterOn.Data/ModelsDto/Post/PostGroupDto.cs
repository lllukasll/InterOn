using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InterOn.Data.ModelsDto.Post
{
    public class PostGroupDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public UserDto User { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public int GroupId { get; set; }

        public ICollection<PostCommentDto> PostComments { get; set; }

        public PostGroupDto()
        {
            PostComments = new Collection<PostCommentDto>();
        }
    }
}