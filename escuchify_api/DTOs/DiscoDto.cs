namespace escuchify_api.DTOs;

public class DiscoDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public int AnioLanzamiento { get; set; }
    public string Genero { get; set; } = string.Empty;
}