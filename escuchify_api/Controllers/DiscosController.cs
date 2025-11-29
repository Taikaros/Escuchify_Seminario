using Microsoft.AspNetCore.Mvc;
using escuchify_api.Services;
using escuchify_api.DTOs;

namespace escuchify_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiscosController : ControllerBase
{
    // ESTA PARTE FALTABA (La conexión con el Servicio)
    private readonly DiscosService _service;

    public DiscosController(DiscosService service)
    {
        _service = service;
    }
    // -------------------------------------------

    [HttpPost("{artistaId}")]
    public async Task<IActionResult> Crear(int artistaId, DiscoDto dto)
    {
        var exito = await _service.CrearDiscoParaArtista(artistaId, dto);
        if (!exito) return NotFound("Artista no encontrado");
        return Ok();
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var disco = await _service.ObtenerPorId(id);
        if (disco == null) return NotFound();
        return Ok(disco);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDisco(int id)
    {
        // Llama al servicio para borrar el disco entero
        var exito = await _service.BorrarDisco(id);
        
        if (!exito) return NotFound("Disco no encontrado");
        
        return NoContent();
    }
    [HttpPost("{discoId}/canciones")]
    public async Task<IActionResult> AgregarCancion(int discoId, CancionDto dto)
    {
        var exito = await _service.AgregarCancion(discoId, dto);

        if (!exito) return NotFound("Disco no encontrado");

        return Ok();
    }
    [HttpDelete("cancion/{id}")]
    public async Task<IActionResult> DeleteCancion(int id)
    {
        // Usamos el método que creaste en el servicio
        var exito = await _service.BorrarCancion(id);
        
        if (!exito) 
        {
            return NotFound("Canción no encontrada");
        }
        
        return NoContent();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDisco(int id, DiscoDto dto)
    {
        if (dto.Id == 0) dto.Id = id;

        if (id != dto.Id) return BadRequest("El ID no coincide");
        
        var exito = await _service.ActualizarDisco(id, dto);
        
        if (!exito) return NotFound();
        
        return NoContent();
    }
    [HttpPut("cancion/{id}")]
    public async Task<IActionResult> PutCancion(int id, CancionDto dto)
    {
        // CORRECCIÓN: Si el DTO llega sin ID, usamos el de la URL
        if (dto.Id == 0) dto.Id = id;

        // Llamamos al servicio
        var exito = await _service.ActualizarCancion(id, dto);
        
        if (!exito) return NotFound();
        
        return NoContent();
    }
}