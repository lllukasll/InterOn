using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using InterOn.Data.DbModels;

namespace InterOn.Data.ModelsDto
{
    public class GroupDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreateDateTime { get; set; }

        public string AvatarUrl { get; set; }

        ///  public int AdminId{ get; set; }
        public ICollection<GroupCategory> SubCategories { get; set; }


        public GroupDto()
        {
            SubCategories = new Collection<GroupCategory>();
        }
    }
}