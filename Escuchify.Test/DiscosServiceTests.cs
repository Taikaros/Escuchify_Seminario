using Xunit;
using Microsoft.EntityFrameworkCore;
using escuchify_api.Data;
using escuchify_api.Services;
using escuchify_api.DTOs;
using escuchify_api.Core.Entities;

namespace Escuchify.Test;

public class DiscosServiceTests
{
    private AppDbContext GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var context = new AppDbContext(options);
        context.Database.EnsureCreated();
        return context;
    }

    [Fact]
    public async Task CrearDisco_DebeVincularloAlArtista()
    {
        // 1. ARRANGE: Crear un artista simulado
        var context = GetDatabaseContext();
        var artista = new Artista { Nombre = "Metallica" };
        context.Artistas.Add(artista);
        await context.SaveChangesAsync();

        var servicio = new DiscosService(context);
        var nuevoDisco = new DiscoDto { Titulo = "Black Album", AnioLanzamiento = 1991 };

        // 2. ACT: Intentar crear el disco para ese artista
        var resultado = await servicio.CrearDiscoParaArtista(artista.Id, nuevoDisco);

        // 3. ASSERT: Verificar que dijo "True" y que se guardó en la BD
        Assert.True(resultado);
        
        var discoEnDb = await context.Discos.Include(d => d.Artistas).FirstAsync();
        Assert.Equal("Black Album", discoEnDb.Titulo);
        Assert.Equal("Metallica", discoEnDb.Artistas[0].Nombre);
    }

    [Fact]
    public async Task AgregarCancion_DebeGuardarseEnElDisco()
    {
        // 1. ARRANGE: Crear un disco simulado
        var context = GetDatabaseContext();
        var disco = new Discos { Titulo = "Thriller" };
        context.Discos.Add(disco);
        await context.SaveChangesAsync();

        var servicio = new DiscosService(context);
        var cancionDto = new CancionDto { Titulo = "Billie Jean", Duracion = "4:54" };

        // 2. ACT: Agregar la canción al disco
        // NOTA: Si este método te da error en rojo, es porque olvidaste copiarlo en DiscosService.cs en el paso anterior.
        var resultado = await servicio.AgregarCancion(disco.Id, cancionDto);

        // 3. ASSERT
        Assert.True(resultado);
        var cancionEnDb = await context.Canciones.FirstAsync();
        Assert.Equal("Billie Jean", cancionEnDb.Titulo);
        Assert.Equal(disco.Id, cancionEnDb.DiscosID);
    }
    [Fact]
    public async Task BorrarDisco_DebeEliminarTambienSusCanciones()
    {
        // 1. ARRANGE
        var context = GetDatabaseContext();
        
        // Creamos un disco con una canción
        var disco = new Discos { Titulo = "Disco Test" };
        var cancion = new Canciones { Titulo = "Canción Test", discos = disco };
        
        context.Discos.Add(disco);
        context.Canciones.Add(cancion);
        await context.SaveChangesAsync();

        var servicio = new DiscosService(context);

        // 2. ACT
        var resultado = await servicio.BorrarDisco(disco.Id);

        // 3. ASSERT
        Assert.True(resultado);
        
        // Verificamos que se borró el disco...
        Assert.Null(await context.Discos.FindAsync(disco.Id));
        
        // ... ¡Y que también se borró la canción! (Borrado en cascada)
        Assert.Empty(await context.Canciones.ToListAsync());
    }
}
