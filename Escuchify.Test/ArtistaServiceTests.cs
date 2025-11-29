using Xunit;
using Microsoft.EntityFrameworkCore;
using escuchify_api.Data;
using escuchify_api.Services;
using escuchify_api.DTOs;
using escuchify_api.Core.Entities;

namespace Escuchify.Test;

public class ArtistasServiceTests
{
    // Este método crea una Base de Datos "de mentira" en la memoria RAM
    // para que cada test arranque limpio y no toque tu archivo escuchify.db real.
    private AppDbContext GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            
        var databaseContext = new AppDbContext(options);
        databaseContext.Database.EnsureCreated();
        return databaseContext;
    }

    [Fact]
    public async Task Crear_DebeGuardarArtistaCorrectamente()
    {
        // 1. ARRANGE (Preparar)
        var context = GetDatabaseContext();
        var servicio = new ArtistasService(context);
        
        var nuevoArtista = new ArtistaDto 
        { 
            Nombre = "Shakira", 
            Genero = "Pop",
            ImagenUrl = "shakira.jpg"
        };

        // 2. ACT (Actuar)
        var resultado = await servicio.Crear(nuevoArtista);

        // 3. ASSERT (Verificar)
        Assert.NotNull(resultado);                 // ¿Devolvió algo?
        Assert.Equal("Shakira", resultado.Nombre); // ¿Se guardó el nombre bien?
        Assert.NotEqual(0, resultado.Id);          // ¿La BD le dio un ID?
    }

    [Fact]
    public async Task ObtenerTodos_DebeRetornarLista()
    {
        // 1. ARRANGE
        var context = GetDatabaseContext();
        // Inyectamos datos falsos
        context.Artistas.Add(new Artista { Nombre = "Bad Bunny", Genero = "Urbano" });
        context.Artistas.Add(new Artista { Nombre = "Adele", Genero = "Pop" });
        await context.SaveChangesAsync();

        var servicio = new ArtistasService(context);

        // 2. ACT
        var lista = await servicio.ObtenerTodos();

        // 3. ASSERT
        Assert.Equal(2, lista.Count); // ¿Hay 2 artistas?
        Assert.Contains(lista, a => a.Nombre == "Bad Bunny"); // ¿Está Bad Bunny?
    }
    [Fact]
    public async Task Actualizar_DebeModificarDatos_SiExiste()
    {
        // 1. ARRANGE
        var context = GetDatabaseContext();
        var artista = new Artista { Nombre = "Viejo Nombre", Genero = "Rock" };
        context.Artistas.Add(artista);
        await context.SaveChangesAsync();

        var servicio = new ArtistasService(context);
        var datosNuevos = new ArtistaDto { Nombre = "Nuevo Nombre", Genero = "Pop" };

        // 2. ACT
        var resultado = await servicio.Actualizar(artista.Id, datosNuevos);

        // 3. ASSERT
        Assert.True(resultado); // Debe decir que sí pudo
        var artistaEnDb = await context.Artistas.FindAsync(artista.Id);
        Assert.Equal("Nuevo Nombre", artistaEnDb.Nombre); // Verificamos el cambio
    }

    [Fact]
    public async Task Borrar_DebeEliminarArtista_YRetornarTrue()
    {
        // 1. ARRANGE
        var context = GetDatabaseContext();
        var artista = new Artista { Nombre = "Artista a Borrar" };
        context.Artistas.Add(artista);
        await context.SaveChangesAsync();

        var servicio = new ArtistasService(context);

        // 2. ACT
        var resultado = await servicio.Borrar(artista.Id);

        // 3. ASSERT
        Assert.True(resultado);
        Assert.Equal(0, await context.Artistas.CountAsync()); // La tabla debe estar vacía
    }

    [Fact]
    public async Task Borrar_DebeRetornarFalse_SiNoExiste()
    {
        // 1. ARRANGE
        var context = GetDatabaseContext();
        var servicio = new ArtistasService(context);

        // 2. ACT (Intentamos borrar el ID 999 que no existe)
        var resultado = await servicio.Borrar(999);

        // 3. ASSERT
        Assert.False(resultado); // Debe avisar que falló
    }
}