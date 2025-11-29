namespace escuchify_api.DTOs;

public class CancionDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Duracion { get; set; } = string.Empty;
    public string Genero { get; set; } = string.Empty;
}