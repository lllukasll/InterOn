using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InterOn.Data.ModelsDto.Event
{
    public class UpdateEventDto
    {      
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateTimeEvent { get; set; }
        
        public int GroupId { get; set; }
        //Admin
        public int UserId { get; set; }
      
        public ICollection<int> SubCategories { get; set; }

        public UpdateEventDto()
        {
            SubCategories = new Collection<int>();
        }
    }
}