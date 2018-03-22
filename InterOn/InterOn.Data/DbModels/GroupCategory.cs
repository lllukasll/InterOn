using System.ComponentModel.DataAnnotations.Schema;

namespace InterOn.Data.DbModels
{
    [Table("GroupCategories")]
    public class GroupCategory
    {
        public int SubCategoryId { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public SubCategory SubCategory { get; set; }

    }
}