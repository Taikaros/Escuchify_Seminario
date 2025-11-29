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
}