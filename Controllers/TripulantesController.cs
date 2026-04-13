/*
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmericanAirlinesApi.Models;
using EcoMonitorApi.Data;

namespace AmericanAirlinesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripulantesControlle(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tripulante>>> Get()
        => await context.Tripulantes.OrderByDescending(a => a.Id).ToListAsync();
        
    [HttpPost]
    public async Task<ActionResult<Tripulante>> Post(Tripulante tp)
    {   
        context.Voos.Add(voo);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), voo);
    }
}*/