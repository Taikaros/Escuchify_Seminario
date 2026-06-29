using Microsoft.EntityFrameworkCore;
using Xunit;
using escuchify_api.Data;
using escuchify_api.Core.Entities;
using System.Linq;

namespace Escuchify.Test
{
    public class CancionesIntegracionTests
    {
        [Fact]
        public void AgregarCancion_ConDiscoAsociado_SeGuardaYRelacionaCorrectamente()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "EscuchifyCancionesDb")
                .Options;

            using (var context = new AppDbContext(options))
            {
                // Usamos 'AnioLanzamiento' como figura en Discos.cs[cite: 5]
                var disco = new Discos { Titulo = "A Night at the Opera", AnioLanzamiento = 1975 }; 
                // Usamos la propiedad 'discos' (en minúscula) definida en Canciones.cs
                var cancion = new Canciones { Titulo = "Bohemian Rhapsody", discos = disco }; 
                
                context.Canciones.Add(cancion);
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                var cancionGuardada = context.Canciones.FirstOrDefault(c => c.Titulo == "Bohemian Rhapsody");
                Assert.NotNull(cancionGuardada);
            }
        }
    }
}