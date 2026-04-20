using System.ComponentModel.DataAnnotations;

namespace Delab.Shared.Entities;

public class City
{
    [Key]
    public int CityId { get; set; }
    public int StateId { get; set; }
   
    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    [MaxLength(100, ErrorMessage = "El campo {0} no puede ser mayor de {1} carácteres")]
    [Display(Name = "Ciudad")]
    public string Name { get; set; } = null!;

    //Relaciones
    public State? State { get; set; }
}
