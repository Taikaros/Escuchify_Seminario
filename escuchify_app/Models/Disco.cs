using System.ComponentModel.DataAnnotations;

namespace escuchify_app.Models;

public class Disco
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El álbum necesita un título")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "El año es obligatorio")]
    public int AnioLanzamiento { get; set; }

    public string Genero { get; set; } = string.Empty;
    public int ArtistaId { get; set; }

    // Esta es la lista que daba error antes
    public List<Cancion> Canciones { get; set; } = new(); 
}