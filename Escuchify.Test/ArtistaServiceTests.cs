using Xunit;
using Moq;
using System.Net.Http;
using escuchify_api.Services;
using escuchify_api.DTOs;
using escuchify_api.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Escuchify.Test
{
    public class ArtistasServiceTests
    {
        // Método auxiliar para crear una base de datos limpia por cada test
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
            // 1. ARRANGE
            var context = GetDatabaseContext();
            
            // Creamos un mock para evitar el error de null en IHttpClientFactory
            var mockFactory = new Mock<IHttpClientFactory>();
            
            // Instanciamos el servicio pasando el contexto y el mock factory
            var servicio = new ArtistasService(context, mockFactory.Object);
            
            var nuevoArtista = new ArtistaDto 
            { 
                Nombre = "Shakira", 
                Genero = "Pop",
                ImagenUrl = "shakira.jpg"
            };

            // 2. ACT
            var resultado = await servicio.Crear(nuevoArtista);

            // 3. ASSERT
            Assert.NotNull(resultado);
            Assert.Equal("Shakira", resultado.Nombre);
            Assert.NotEqual(0, resultado.Id);
        }

        [Fact]
        public async Task Borrar_DebeEliminarArtista_YRetornarTrue()
        {
            // 1. ARRANGE
            var context = GetDatabaseContext();
            var mockFactory = new Mock<IHttpClientFactory>();
            
            var artista = new escuchify_api.Core.Entities.Artista { Nombre = "Artista a Borrar" };
            context.Artistas.Add(artista);
            await context.SaveChangesAsync();

            var servicio = new ArtistasService(context, mockFactory.Object);

            // 2. ACT
            var resultado = await servicio.Borrar(artista.Id);

            // 3. ASSERT
            Assert.True(resultado);
            Assert.Equal(0, await context.Artistas.CountAsync());
        }

        [Fact]
        public async Task Actualizar_DebeModificarNombreCorrectamente()
        {
            // 1. ARRANGE
            var context = GetDatabaseContext();
            var mockFactory = new Mock<IHttpClientFactory>();
            
            var artista = new escuchify_api.Core.Entities.Artista { Nombre = "Nombre Viejo" };
            context.Artistas.Add(artista);
            await context.SaveChangesAsync();

            var servicio = new ArtistasService(context, mockFactory.Object);
            var datosNuevos = new ArtistaDto { Nombre = "Nombre Nuevo", Genero = "Pop" };

            // 2. ACT
            var resultado = await servicio.Actualizar(artista.Id, datosNuevos);

            // 3. ASSERT
            Assert.True(resultado);
            var artistaEnDb = await context.Artistas.FindAsync(artista.Id);
            Assert.Equal("Nombre Nuevo", artistaEnDb.Nombre);
        }
    }
}