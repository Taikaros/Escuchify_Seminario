using System.Text.Json.Serialization;
using Escuchify.Modelos; // Asegúrate de usar el namespace correcto

namespace Escuchify.Modelos 
{
    public class Canciones
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Duracion { get; set; }
        public string Genero { get; set; }

        // CORRECCIÓN 2: Clave foránea correcta
        public int DiscosID { get; set; }

        // CORRECCIÓN 3: Propiedad de navegación del tipo 'Discos', no 'object'
        [JsonIgnore] // Importante para que no intente traer el Disco entero infinitamente al pedir una canción
        public Discos? discos { get; set; }

        public Canciones() { } // Constructor vacío para EF

        public Canciones(int id, string titulo, string duracion, string genero)
        {
            Id = id;
            Titulo = titulo;
            Duracion = duracion;
            Genero = genero;
        }
    }
}