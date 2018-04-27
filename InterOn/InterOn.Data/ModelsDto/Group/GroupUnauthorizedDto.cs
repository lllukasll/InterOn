using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using InterOn.Data.ModelsDto.Category;

namespace InterOn.Data.ModelsDto.Group
{
    public class GroupUnauthorizedDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreateDateTime { get; set; }

        public string AvatarUrl { get; set; }

        public int NumberOfUsers { get; set; }

        //public ICollection<UserGroupDto> Users { get; set; }
        public ICollection<SubCategoriesDto> SubCategoriesDtos { get; set; }

        public GroupUnauthorizedDto()
        {
            SubCategoriesDtos = new Collection<SubCategoriesDto>();
          //  Users = new Collection<UserGroupDto>();
         }
    }
}