using System.Net.Http.Json;
using escuchify_app.Models;

namespace escuchify_app.Services;

public class ApiService
{
    private readonly HttpClient _http;

    public ApiService(HttpClient http)
    {
        _http = http;
    }

    // --- ARTISTAS ---
    public async Task<List<Artista>> ObtenerArtistas()
    {
        try 
        {
            // OJO: "api/artistas" debe coincidir con tu Controller en la API
            var respuesta = await _http.GetFromJsonAsync<List<Artista>>("api/artistas");
            return respuesta ?? new List<Artista>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return new List<Artista>();
        }
    }

    public async Task CrearArtista(Artista artista)
    {
        await _http.PostAsJsonAsync("api/artistas", artista);
    }
    public async Task<Artista?> ObtenerArtistaPorId(int id)
    {
    try
    {
        return await _http.GetFromJsonAsync<Artista>($"api/artistas/{id}");
    }
    catch
    {
        return null;
    }
    }
    public async Task CrearDisco(int artistaId, Disco disco)
    {
    // Enviamos el disco a la ruta: api/discos/{id del artista}
    await _http.PostAsJsonAsync($"api/discos/{artistaId}", disco);
    }
}