using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterOn.Data.DbModels
{
    [Table("SubCategories")]
    public class SubCategory : BaseEntity
    {
        
        public string Name { get; set; }

        public int MainCategoryId { get; set; }
        public MainCategory MainCategory { get; set; }

        public ICollection<GroupCategory> Groups { get; set; }


        public SubCategory()
        {
            Groups=new Collection<GroupCategory>();
        }


    }
}