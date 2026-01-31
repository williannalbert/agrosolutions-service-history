using AgroSolutions.History.Application.DTOs;
using AgroSolutions.History.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AgroSolutions.History.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HistoryController : ControllerBase
{
    private readonly ISensorService _sensorService;

    public HistoryController(ISensorService sensorService)
    {
        _sensorService = sensorService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateReadingRequest request)
    {
        try
        {
            var result = await _sensorService.RegisterReadingAsync(request);

            return CreatedAtAction(nameof(Post), new { id = result.Id }, result);
        }
        catch (ArgumentException ex) 
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex) 
        {
            return StatusCode(500, new { error = "Erro interno ao processar leitura.", details = ex.Message });
        }
    }
}
