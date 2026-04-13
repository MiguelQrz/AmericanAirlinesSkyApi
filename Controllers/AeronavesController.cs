using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmericanAirlinesApi.Models;
using EcoMonitorApi.Data;

namespace AmericanAirlinesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AeronavesController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Aeronave>>> Get()
        => await context.Aeronaves.OrderByDescending(a => a.Id).ToListAsync();
        
    [HttpPost]
    public async Task<ActionResult<Aeronave>> Post(Aeronave aeronave)
    {
        if (context.Aeronaves.Find(aeronave.CodigoCauda) != null)
        {
            return BadRequest("Não é possível cadastrar uma aeronave com um código de cauda já existente.");
        }
        context.Aeronaves.Add(aeronave);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), aeronave);
    }
}