using System;

namespace InterOn.Data.ModelsDto.Post
{
    public class CreateGroupPostDto
    {
        public int Id{ get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int GroupId { get; set; }
    }
}