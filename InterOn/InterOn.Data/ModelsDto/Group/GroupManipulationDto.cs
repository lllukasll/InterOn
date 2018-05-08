using System.ComponentModel.DataAnnotations;

namespace InterOn.Data.ModelsDto.Group
{
    public abstract class GroupManipulationDto
    {
        [Required(ErrorMessage = "Nazwa grupy jest wymagana")]
        [StringLength(50, ErrorMessage = "Nazwa Grupy może się składać z maksymalnie 50 znaków")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Opis jest wymagany")]
        [StringLength(500, ErrorMessage = "Opis może się składać z maksymalnie 500 znaków")]
        public string Description { get; set; }
    }
}