using System.ComponentModel.DataAnnotations;

namespace escuchify_api.DTOs;

public class DiscoDto
{
    public int Id { get; set; }

    [Required]
    public string Titulo { get; set; } = string.Empty;

    [Range(1900, 2100, ErrorMessage = "Año inválido")]
    public int AnioLanzamiento { get; set; }

    [Required]
    public string Genero { get; set; } = string.Empty;
    
    public int ArtistaId { get; set; }
    public List<CancionDto> Canciones { get; set; } = new();
}