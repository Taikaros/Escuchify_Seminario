namespace escuchify_app.Models;

public class Artista
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Genero { get; set; } = string.Empty;
    public string ImagenUrl { get; set; } = string.Empty;
    // Si agregaste más propiedades en la API, agrégalas aquí también
    public List<Disco> Discos { get; set; } = new();
}