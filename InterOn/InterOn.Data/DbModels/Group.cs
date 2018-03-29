using System;
using System.Collections;
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

        public string AvatarUrl { get; set; }

       ///  public int AdminId{ get; set; }
        public ICollection<GroupCategory> SubCategories { get; set; }


        public Group()
        {
            SubCategories = new Collection<GroupCategory>();
        }
    }
}