using System;
using System.ComponentModel.DataAnnotations;

namespace InterOn.Data.ModelsDto.Comments
{
    public class CreateGroupPostCommentDto : ContentManipulationDto
    {
        [MaxLength(500,ErrorMessage = "W tym polu można jedynie użyć 500 znaków")]
        public override string Content
        {
            get => base.Content;

            set => base.Content = value;
        }
        public DateTime CreateDateTime { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}
