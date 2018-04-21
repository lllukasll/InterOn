using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

        public ICollection<UserGroupDto> Users { get; set; }

        public GroupUnauthorizedDto()
        {
            Users = new Collection<UserGroupDto>();
         }
    }
}