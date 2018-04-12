using System.ComponentModel.DataAnnotations;

namespace InterOn.Data.DbModels
{
    public class Address : BaseEntity
    {
        [Required]
        public string Longitude { get; set; }
        [Required]
        public string Latitude { get; set; }

        public int EventRef { get; set; }
        public Event Event { get; set; }
    }
}