using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InterOn.Data.ModelsDto.Group
{
    public class UpdateGroupDto
    {
        
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        ///  public int AdminId{ get; set; }
        public ICollection<int> SubCategories { get; set; }

        public ICollection<int> Users;

        public UpdateGroupDto(ICollection<int> users)
        {
            Users = new Collection<int>();
            SubCategories = new Collection<int>();
        }
        
    }
}