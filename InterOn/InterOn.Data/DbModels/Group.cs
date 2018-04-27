using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterOn.Data.DbModels
{
    [Table("Groups")]
    public class Group : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        public DateTime CreateDateTime { get; set; }

        public string GroupPhoto{ get; set; }
        
        //admin
        public int? UserId { get; set; }
        public User User { get; set; }
        
        public ICollection<GroupCategory> SubCategories { get; set; }

        public ICollection<UserGroup> Users { get; set; }
        public ICollection<Post> Posts { get; set; }

        public Group()
        {
            Posts = new Collection<Post>();
            Users = new Collection<UserGroup>();
            SubCategories = new Collection<GroupCategory>();
        }
    }
}