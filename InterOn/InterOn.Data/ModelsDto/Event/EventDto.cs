using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using InterOn.Data.ModelsDto.Category;
using InterOn.Data.ModelsDto.Group;

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
        public int UserId { get; set; }
        public int NumberOfUsers { get; set; }

        public ICollection<SubCategoriesDto> SubCategories { get; set; }
        public ICollection<UserGroupDto> Users { get; set; }

        public EventDto()
        {
            Users = new Collection<UserGroupDto>();
            SubCategories = new Collection<SubCategoriesDto>();
        }
    }
}