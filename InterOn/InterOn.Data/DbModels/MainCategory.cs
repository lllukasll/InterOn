using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterOn.Data.DbModels
{
    [Table("MainCategories")]
    public class MainCategory : BaseEntity
    {
       // public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public ICollection<SubCategory> SubCategories { get; set; }

        public MainCategory()
        {
            SubCategories = new Collection<SubCategory>();
        }
    }
}