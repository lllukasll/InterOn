using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace InterOn.Data.DbModels
{
    public class Event : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        public DateTime DateTimeEvent { get; set; }

        public string PhotoUrl { get; set; }

        public Address Address { get; set; }

        public int  GroupId { get; set; }
        public Group Group { get; set; }

        //Admin
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Post> Posts { get; set; }

        public ICollection<EventSubCategory> SubCategories { get; set; }
        public Event()
        {
            Posts = new Collection<Post>();
            SubCategories = new Collection<EventSubCategory>();
        }
    }
}