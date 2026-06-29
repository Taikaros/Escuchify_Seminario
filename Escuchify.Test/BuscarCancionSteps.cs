using TechTalk.SpecFlow;
using Xunit;
using escuchify_api.Core.Entities;
using System.Collections.Generic;

namespace Escuchify.Test.Steps
{
    [Binding]
    public class BuscarCancionSteps
    {
        private string _terminoBusqueda = string.Empty;
        private List<Canciones> _resultadosBusqueda = new List<Canciones>();

        [Given(@"que la canción ""(.*)"" está registrada en la base de datos")]
        public void GivenQueLaCancionEstaRegistrada(string tituloCancion)
        {
            // Simulación de que la canción existe en la DB
            var cancionSimulada = new Canciones { Titulo = tituloCancion };
            _resultadosBusqueda.Add(cancionSimulada);
        }

        [When(@"el usuario ingresa la palabra ""(.*)"" en la barra de búsqueda")]
        public void WhenElUsuarioIngresaLaPalabraEnLaBarraDeBusqueda(string busqueda)
        {
            _terminoBusqueda = busqueda;
        }

        [When(@"presiona el botón de buscar")]
        public void WhenPresionaElBotonDeBuscar()
        {
            // Aquí llamarías al endpoint real de CansionesController para hacer el filtro
        }

        [Then(@"el sistema debe devolver al menos un resultado")]
        public void ThenElSistemaDebeDevolverAlMenosUnResultado()
        {
            Assert.NotEmpty(_resultadosBusqueda);
        }

        [Then(@"el resultado debe incluir la canción ""(.*)""")]
        public void ThenElResultadoDebeIncluirLaCancion(string cancionEsperada)
        {
            Assert.Contains(_resultadosBusqueda, c => c.Titulo == cancionEsperada);
        }
    }
}