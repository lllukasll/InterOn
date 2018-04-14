using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using InterOn.Data.DbModels;

namespace InterOn.Data.ModelsDto.Group
{
    public class GroupDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreateDateTime { get; set; }

        public GroupPhoto GroupPhoto { get; set; }
        ///  public int AdminId{ get; set; }
        public ICollection<GroupSubCategoryDto> SubCategories { get; set; }
        public ICollection<UserGroupDto> Users { get; set; }

        public GroupDto()
        {
            Users =new Collection<UserGroupDto>();
            SubCategories = new Collection<GroupSubCategoryDto>();
        }
    }
}