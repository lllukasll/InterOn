using System.ComponentModel.DataAnnotations;

namespace InterOn.Data.DbModels
{
    public class SubCategoryPhoto : BaseEntity
    {
        [Required]
        [StringLength(255)]
        public string FileName { get; set; }

        public int SubCategoryRef { get; set; }
        public SubCategory SubCategory { get; set; }
    }
}