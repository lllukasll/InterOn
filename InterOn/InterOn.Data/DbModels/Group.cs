using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterOn.Data.DbModels
{
    [Table("Groups")]
    public class Group : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreateDateTime { get; set; }

        public GroupPhoto GroupPhoto{ get; set; }

        // public int AdminId{ get; set; }

        public ICollection<GroupCategory> SubCategories { get; set; }

        public ICollection<UserGroup> Users { get; set; }

        public Group()
        {
            Users = new Collection<UserGroup>();
            SubCategories = new Collection<GroupCategory>();
        }
    }
}