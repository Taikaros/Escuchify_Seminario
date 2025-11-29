namespace escuchify_app.Models;

public class Disco
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public int AnioLanzamiento { get; set; }
    public string Genero { get; set; } = string.Empty;

}