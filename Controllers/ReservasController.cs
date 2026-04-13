using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmericanAirlinesApi.Models;
using EcoMonitorApi.Data;

namespace AmericanAirlinesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservasController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Reserva>>> Get()
        => await context.Reservas.OrderByDescending(a => a.Id).ToListAsync();
        
    [HttpPost]
    public async Task<ActionResult<Reserva>> Post(Reserva reserva)
    {
        var voo = context.Voos.Find(reserva.VooId);
        
        if (voo == null)
        {
            return NotFound("Não foi possível realizar sua reserva, esse voô não existe.");
        }else{
            var aeronave = context.Aeronaves.Find(voo.AeronaveId);
            var reservas = context.Reservas.Where(r => r.VooId == reserva.VooId).Count();
            if (!voo.Status.ToLower().Equals("agendado"))
            {
                return BadRequest("Não foi possível realizar sua reserva, esse voô já decolou ou foi cancelado.");
            }else if (aeronave!.CapacidadePassageiros == reservas)
            {
                return BadRequest("Não foi possível cadastrar sua reserva, esse voô já está lotado.");
            }else if (reserva.Assento.EndsWith("A") || reserva.Assento.EndsWith("F"))
            {
                Console.WriteLine("");
            }
        context.Reservas.Add(reserva);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), reserva);
    }
}}