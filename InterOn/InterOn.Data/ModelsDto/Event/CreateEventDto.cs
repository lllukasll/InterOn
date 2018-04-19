using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InterOn.Data.ModelsDto.Event
{
    public class CreateEventDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateTimeEvent { get; set; }
        public string PhotoUrl { get; set; }
        public int GroupId { get; set; }
        //Admin
        //public int UserId { get; set; }
        //public User User { get; set; }

        public ICollection<int> SubCategories { get; set; }

        public CreateEventDto()
        {
            SubCategories = new Collection<int>();
        }
    }
}