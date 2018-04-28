using System;
using InterOn.Data.DbModels;

namespace InterOn.Data.ModelsDto.Comments
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
       
        public UserDto User { get; set; }
        
    }
}