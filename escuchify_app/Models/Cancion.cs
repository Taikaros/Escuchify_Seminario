using System.ComponentModel.DataAnnotations;

namespace escuchify_app.Models;

public class Cancion
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El título es obligatorio")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "La duración es obligatoria")]
    [RegularExpression(@"^\d{1,2}:\d{2}$", ErrorMessage = "Formato: mm:ss (ej: 04:20)")]
    public string Duracion { get; set; } = string.Empty;

    public string Genero { get; set; } = string.Empty;
}