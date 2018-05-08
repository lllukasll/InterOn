using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InterOn.Data.ModelsDto.Group
{
    public class UpdateGroupDto : GroupManipulationDto
    { 
        public int Id { get; set; }

        ///  public int AdminId{ get; set; }
        public ICollection<int> SubCategories { get; set; }

        public ICollection<int> Users { get; set; }

        public UpdateGroupDto()
        {
            Users = new Collection<int>();
            SubCategories = new Collection<int>();
        }  
    }
}