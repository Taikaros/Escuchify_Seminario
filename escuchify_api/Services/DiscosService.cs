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
}