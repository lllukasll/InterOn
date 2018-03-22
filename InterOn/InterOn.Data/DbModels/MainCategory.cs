using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterOn.Data.DbModels
{
    [Table("MainCategories")]
    public class MainCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<SubCategory> SubCategories { get; set; }

        public MainCategory()
        {
            SubCategories = new List<SubCategory>();
        }
    }
}