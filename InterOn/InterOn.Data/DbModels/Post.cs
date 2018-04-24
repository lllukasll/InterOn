using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace InterOn.Data.DbModels
{
    
    public class Post : BaseEntity
    {
        [Required]
        public string Content { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }

        public int? EventId { get; set; }
        public Event Event { get; set; }

        public int? GroupId { get; set; }
        public Group Group { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public Post()
        {
            Comments = new Collection<Comment>();
        }

    }
}