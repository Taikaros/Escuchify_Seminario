 // Necesario para evitar ciclos al serializar si usas API

namespace Escuchify.Modelos
{
    using System.Text.Json.Serialization;
    public class Discos
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int AnioLanzamiento { get; set; }
        public string Genero { get; set; }
        public string SelloDiscografico { get; set; }
        public string tipodisco { get; set; }

        // CORRECCIÓN 1: Debe ser una Colección de Canciones, no 'object'
        // [JsonIgnore] // Descomenta esto si al hacer GET te da error de "Cycle detection"
        public ICollection<Canciones> Canciones { get; set; } 

        public Discos() { } // EF Core necesita un constructor vacío a veces

        public Discos(int id, string titulo, int anioLanzamiento, string genero, string selloDiscografico, string TipoDisco)
        {
            Id = id;
            Titulo = titulo;
            tipodisco = TipoDisco;
            AnioLanzamiento = anioLanzamiento;
            Genero = genero;
            SelloDiscografico = selloDiscografico;
            Canciones = new List<Canciones>(); // Inicializar la lista
        }
    }
}