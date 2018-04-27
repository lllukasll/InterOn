using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterOn.Data.DbModels
{
    [Table("SubCategories")]
    public class SubCategory : BaseEntity
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public int MainCategoryId { get; set; }
        public MainCategory MainCategory { get; set; }

        public string SubCategoryPhoto { get; set; }

        public ICollection<GroupCategory> Groups { get; set; }
        public ICollection<EventSubCategory> Events { get; set; }

        public SubCategory()
        {
            Events=new Collection<EventSubCategory>();
            Groups=new Collection<GroupCategory>();
        }
    }
}