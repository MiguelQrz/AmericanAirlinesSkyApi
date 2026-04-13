using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmericanAirlinesApi.Models;
using EcoMonitorApi.Data;

namespace AmericanAirlinesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VoosController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Voo>>> Get()
        => await context.Voos.OrderByDescending(a => a.Id).ToListAsync();
        
    [HttpPost]
    public async Task<ActionResult<Voo>> Post(Voo voo)
    {   
        var aeronave = context.Aeronaves.Find(voo.AeronaveId);
        if (aeronave == null)
        {
            return BadRequest("Essa aeronave não existe.");
        }
        context.Voos.Add(voo);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), voo);
    }
}