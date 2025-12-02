using System.ComponentModel.DataAnnotations;

namespace escuchify_api.DTOs;

public class CancionDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El título de la canción es obligatorio")]
    [StringLength(100, ErrorMessage = "El título no puede superar los 100 caracteres")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "La duración es obligatoria")]
    [RegularExpression(@"^\d{1,2}:\d{2}$", ErrorMessage = "El formato debe ser mm:ss (ej: 03:45)")]
    public string Duracion { get; set; } = string.Empty;

    [Required(ErrorMessage = "El género es obligatorio")]
    public string Genero { get; set; } = string.Empty;
}