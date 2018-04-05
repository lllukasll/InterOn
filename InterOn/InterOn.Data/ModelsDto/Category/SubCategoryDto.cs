using System.Collections.Generic;
using System.Collections.ObjectModel;
using InterOn.Data.DbModels;

namespace InterOn.Data.ModelsDto.Category
{
    public class SubCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MainCategoryId { get; set; }
        
    }
}