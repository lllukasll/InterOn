using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterOn.Data.DbModels
{
    [Table("SubCategories")]
    public class SubCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int MainCategoryId { get; set; }
        public MainCategory MainCategory { get; set; }

        public ICollection<GroupCategory> Group { get; set; }


        public SubCategory()
        {
            Group=new Collection<GroupCategory>();
        }


    }
}