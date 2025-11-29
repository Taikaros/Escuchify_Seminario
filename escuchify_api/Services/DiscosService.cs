using escuchify_api.Data;
using escuchify_api.Core.Entities;
using escuchify_api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace escuchify_api.Services;

public class DiscosService
{
    // ESTA PARTE FALTABA (La conexión con la BD)
    private readonly AppDbContext _context;

    public DiscosService(AppDbContext context)
    {
        _context = context;
    }
    // -------------------------------------------

    public async Task<bool> CrearDiscoParaArtista(int artistaId, DiscoDto dto)
    {
        var artista = await _context.Artistas.FindAsync(artistaId);
        if (artista == null) return false;

        var nuevoDisco = new Discos
        {
            Titulo = dto.Titulo,
            AnioLanzamiento = dto.AnioLanzamiento,
            Genero = dto.Genero,
            Artistas = new List<Artista> { artista }
        };

        _context.Discos.Add(nuevoDisco);
        await _context.SaveChangesAsync();
        return true;
    }
    // Método nuevo para agregar canciones
    public async Task<bool> AgregarCancion(int discoId, CancionDto dto)
    {
        var disco = await _context.Discos.FindAsync(discoId);
        if (disco == null) return false;

        var nuevaCancion = new Canciones
        {
            Titulo = dto.Titulo,
            Duracion = dto.Duracion,
            Genero = dto.Genero,
            DiscosID = discoId
        };

        _context.Canciones.Add(nuevaCancion);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<DiscoDto?> ObtenerPorId(int id)
    {
        var disco = await _context.Discos
            .Include(d => d.Canciones) // Traemos las canciones
            .Include(d => d.Artistas)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (disco == null) return null;

        // Mapeo manual rápido (o usa AutoMapper si sabes)
        return new DiscoDto
        {
            Id = disco.Id,
            Titulo = disco.Titulo,
            AnioLanzamiento = disco.AnioLanzamiento,
            Genero = disco.Genero,

            ArtistaId = disco.Artistas.FirstOrDefault()?.Id ?? 0,
            Canciones = disco.Canciones.Select(c => new CancionDto
            {
                Id = c.Id,
                Titulo = c.Titulo,
                Duracion = c.Duracion
            }).ToList()
        };
    }
    
    // ... otros métodos ...

    public async Task<bool> BorrarCancion(int id)
    {
        var cancion = await _context.Canciones.FindAsync(id);
        if (cancion == null) return false;

        _context.Canciones.Remove(cancion);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> ActualizarDisco(int id, DiscoDto dto)
    {
        var disco = await _context.Discos.FindAsync(id);
        if (disco == null) return false;

        disco.Titulo = dto.Titulo;
        disco.AnioLanzamiento = dto.AnioLanzamiento;
        disco.Genero = dto.Genero;

        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> ActualizarCancion(int id, CancionDto dto)
    {
        var cancion = await _context.Canciones.FindAsync(id);
        if (cancion == null) return false;

        cancion.Titulo = dto.Titulo;
        cancion.Duracion = dto.Duracion;

        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> BorrarDisco(int id)
    {
        var disco = await _context.Discos.FindAsync(id);
        if (disco == null) return false;

        _context.Discos.Remove(disco);
        await _context.SaveChangesAsync();
        return true;
    }
}