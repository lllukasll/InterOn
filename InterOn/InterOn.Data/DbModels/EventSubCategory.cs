using System.ComponentModel.DataAnnotations.Schema;

namespace InterOn.Data.DbModels
{
    [Table("EventSubCategories")]
    public class EventSubCategory
    {
        public int EventId { get; set; }
        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
        public Event Event { get; set; }
    }
}