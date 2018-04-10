using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InterOn.Data.ModelsDto.Group
{
    public class GroupDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreateDateTime { get; set; }

        ///  public int AdminId{ get; set; }
        public ICollection<GroupSubCategoryDto> SubCategories { get; set; }


        public GroupDto()
        {
            SubCategories = new Collection<GroupSubCategoryDto>();
        }
    }
}