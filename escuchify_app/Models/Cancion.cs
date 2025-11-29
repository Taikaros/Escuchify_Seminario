namespace escuchify_app.Models;
public class Cancion {
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Duracion { get; set; } = string.Empty;
    public string Genero { get; set; } = string.Empty;
}