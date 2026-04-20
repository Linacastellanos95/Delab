using System.ComponentModel.DataAnnotations;

namespace Delab.Shared.Entities;

public class State
{
    [Key]
    public int StateId { get; set; }
    public int CountryId { get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    [MaxLength(100, ErrorMessage = "El campo {0} no puede ser mayor de {1} carácteres")]
    [Display(Name = "Depto/Estado")]
    public string Name { get; set; } = null!;

    //Relaciones
    public Country? Country { get; set; }
    public ICollection<City>? Cities { get; set; }

}
