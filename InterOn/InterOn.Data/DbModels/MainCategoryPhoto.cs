using System.ComponentModel.DataAnnotations;

namespace InterOn.Data.DbModels
{
    public class MainCategoryPhoto :BaseEntity
    {
        [Required]
        [StringLength(255)]
        public string FileName { get; set; }

        public int MainCategoryRef { get; set; }
        public MainCategory MainCategory { get; set; }
    }
}