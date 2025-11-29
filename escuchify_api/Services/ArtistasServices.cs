using escuchify_api.Data;
using escuchify_api.Core.Entities;
using escuchify_api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace escuchify_api.Services;

public class ArtistasService
{
    private readonly AppDbContext _context;

    public ArtistasService(AppDbContext context)
    {
        _context = context;
    }

    // Método para OBTENER todos los artistas
    public async Task<List<ArtistaDto>> ObtenerTodos()
    {
        var artistas = await _context.Artistas.ToListAsync();
        
        // Convertimos de Entidad (BD) a DTO (Público)
        return artistas.Select(a => new ArtistaDto {
            Id = a.Id,
            Nombre = a.Nombre,
            Genero = a.Genero,
            ImagenUrl = a.ImagenUrl
        }).ToList();
    }

    // Método para CREAR un artista nuevo
    public async Task<Artista> Crear(ArtistaDto dto)
    {
        var nuevoArtista = new Artista {
            Nombre = dto.Nombre,
            Genero = dto.Genero,
            ImagenUrl = dto.ImagenUrl,
            Biografia = "Sin biografía" // Valor por defecto
        };

        _context.Artistas.Add(nuevoArtista);
        await _context.SaveChangesAsync();
        return nuevoArtista;
    }
    // Método para traer un solo artista con sus discos
public async Task<ArtistaDto?> ObtenerPorId(int id)
{
    var artista = await _context.Artistas
        .Include(a => a.Discos) // <--- ¡MAGIA! Trae los discos relacionados
        .FirstOrDefaultAsync(a => a.Id == id);

    if (artista == null) return null;

    return new ArtistaDto
    {
        Id = artista.Id,
        Nombre = artista.Nombre,
        Genero = artista.Genero,
        ImagenUrl = artista.ImagenUrl,
        // Convertimos los discos de la BD a DTOs
        Discos = artista.Discos.Select(d => new DiscoDto {
            Id = d.Id,
            Titulo = d.Titulo,
            AnioLanzamiento = d.AnioLanzamiento,
            Genero = d.Genero
        }).ToList()
    };
}
// Método para BORRAR (Delete)
public async Task<bool> Borrar(int id)
{
    // 1. Buscamos si existe
    var artista = await _context.Artistas.FindAsync(id);
    if (artista == null) return false;

    // 2. Lo borramos (EF Core se encarga del SQL)
    _context.Artistas.Remove(artista);
    await _context.SaveChangesAsync();
    return true;
}
public async Task<bool> Actualizar(int id, ArtistaDto dto)
    {
        var artista = await _context.Artistas.FindAsync(id);
        if (artista == null) return false;

        artista.Nombre = dto.Nombre;
        artista.Genero = dto.Genero;
        artista.ImagenUrl = dto.ImagenUrl;

        await _context.SaveChangesAsync();
        return true;
    }
}