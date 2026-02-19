using AgroSolutions.History.Application.DTOs.Requests;
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
        var result = await _sensorService.RegisterReadingAsync(request);
        return CreatedAtAction(nameof(Post), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetReadingsQuery query)
    {
        var result = await _sensorService.GetReadingsAsync(query);
        return Ok(result);
    }
}
