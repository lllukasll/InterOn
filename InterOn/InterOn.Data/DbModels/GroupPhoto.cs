using System.ComponentModel.DataAnnotations;

namespace InterOn.Data.DbModels
{
    public class GroupPhoto : BaseEntity
    {
        [Required]
        [StringLength(255)]
        public string FileName { get; set; }

        public int GroupRef { get; set; }
        public Group Group { get; set; }
    }
}