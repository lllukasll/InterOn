using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using InterOn.Data.ModelsDto.Category;

namespace InterOn.Data.ModelsDto.Event
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateTimeEvent { get; set; }
        public string PhotoUrl { get; set; }
        public int GroupId { get; set; }
        public AddressDto AddressDto { get; set; }

        public ICollection<SubCategoriesDto> SubCategories { get; set; }

        public EventDto()
        {
            SubCategories = new Collection<SubCategoriesDto>();
        }
    }
}