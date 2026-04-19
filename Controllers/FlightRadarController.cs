using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmericanAirlinesApi.Data;

namespace AmericanAirlinesApi.Controllers;

[ApiController]
[Route("api/radar")]
public class FlightRadarController(AppDbContext context) : ControllerBase
{
    [HttpGet("proximos-destinos")]
    public async Task<ActionResult<Dictionary<string, int>>> GetProximosDestinos()
    {
        await Task.Delay(2000); 
        var voosAtivos = await context.Voos.Where(v => v.Status == "Em Voo").ToListAsync();
        var destinos = voosAtivos.GroupBy(v => v.Destino)
                                 .ToDictionary(g => g.Key, g => g.Count());
        return Ok(destinos);
    }
}