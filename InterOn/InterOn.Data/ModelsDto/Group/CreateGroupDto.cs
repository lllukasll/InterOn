using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InterOn.Data.ModelsDto.Group
{
    public class CreateGroupDto : GroupManipulationDto
    {
        public int Id { get; set; }

        public DateTime CreateDateTime { get; set; }

        public ICollection<int> SubCategories { get; set; }
                
        public CreateGroupDto()
        { 
            SubCategories = new Collection<int>();
        }

    }
}