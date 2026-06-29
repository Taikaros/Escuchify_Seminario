using Xunit;
using escuchify_api.Core.Entities;

namespace Escuchify.Test
{
    public class CancionesTests
    {
        [Fact]
        public void Cancion_CrearInstancia_AsignaPropiedadesCorrectamente()
        {
            var cancion = new Canciones();
            cancion.Titulo = "Bohemian Rhapsody";
            cancion.Duracion = "5:55"; // Debe ser string según tu entidad[cite: 4]

            Assert.NotNull(cancion);
            Assert.Equal("Bohemian Rhapsody", cancion.Titulo);
            Assert.Equal("5:55", cancion.Duracion);
        }
    }
}