using System.ComponentModel.DataAnnotations; 

namespace escuchify_api.DTOs;

public class ArtistaDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, ErrorMessage = "El nombre es muy largo")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El género es obligatorio")]
    public string Genero { get; set; } = string.Empty;

    public string ImagenUrl { get; set; } = string.Empty;
    public string? Biografia { get; set; }
    public List<DiscoDto> Discos { get; set; } = new();
}