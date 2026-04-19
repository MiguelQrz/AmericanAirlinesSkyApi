using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmericanAirlinesApi.Models;
using AmericanAirlinesApi.Data;

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
        var vooAtivo = await context.Voos.AnyAsync(v => v.AeronaveId == voo.AeronaveId && v.Status == "Em Voo");
        if (vooAtivo)
        {
            return Conflict("Aeronave indisponível, encontra-se em trânsito.");
        }
        context.Voos.Add(voo);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), voo);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(int id, [FromBody] string status)
    {
        var voo = await context.Voos.FindAsync(id);
        if (voo == null) return NotFound();
        if ((voo.Status == "Finalizado" || voo.Status == "Cancelado") && status == "Em Voo")
            return BadRequest("Não é possível alterar o status de um voo finalizado ou cancelado para 'Em Voo'.");
        voo.Status = status;
        await context.SaveChangesAsync();
        return NoContent();
    }
}