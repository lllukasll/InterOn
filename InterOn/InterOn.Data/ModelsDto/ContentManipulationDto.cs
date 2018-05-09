using System.ComponentModel.DataAnnotations;

namespace InterOn.Data.ModelsDto
{
    public abstract class ContentManipulationDto
    {
        [Required(ErrorMessage = "To pole nie może być puste")]
        public virtual string Content { get; set; }
    }
}