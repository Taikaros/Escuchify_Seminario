using System.ComponentModel.DataAnnotations;

namespace escuchify_app.Models;

public class Artista
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "¡El género es obligatorio!")]
    public string Genero { get; set; } = string.Empty;

    public string ImagenUrl { get; set; } = string.Empty;
    public string? Biografia { get; set; }
    
    public List<Disco> Discos { get; set; } = new();
}