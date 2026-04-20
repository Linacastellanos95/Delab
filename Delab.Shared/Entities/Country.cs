using System.ComponentModel.DataAnnotations;

namespace Delab.Shared.Entities;

public class Country
{
    [Key]
    public int CountryId { get; set; }
    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    [MaxLength(100, ErrorMessage = "El campo {0} no puede ser mayor de {1} carácteres")]
    [Display(Name  = "País")]
    public string Name { get; set; } = null!;

    [MaxLength(100, ErrorMessage = "El campo {0} no puede ser mayor de {1} carácteres")]
    [Display(Name = "Cod Phone")]
    public string? CodPhone { get; set; }

    //Relaciones
    public ICollection<State>? States { get; set; }
}
