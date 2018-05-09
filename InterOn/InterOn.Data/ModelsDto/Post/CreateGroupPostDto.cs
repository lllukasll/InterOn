using System;

namespace InterOn.Data.ModelsDto.Post
{
    public class CreateGroupPostDto : ContentManipulationDto
    {
        public int Id{ get; set; }
        public int UserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int GroupId { get; set; }
    }
}