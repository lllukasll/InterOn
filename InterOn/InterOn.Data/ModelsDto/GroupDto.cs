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

        public string AvatarUrl { get; set; }

        ///  public int AdminId{ get; set; }
        public ICollection<int> SubCategory { get; set; }

        public GroupDto()
        {
            SubCategory = new Collection<int>();
        }
        
    }
}