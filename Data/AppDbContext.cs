using Microsoft.EntityFrameworkCore;
using AmericanAirlinesApi.Models;

namespace AmericanAirlinesApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Voo> Voos => Set<Voo>();
    public DbSet<Tripulante> Tripulantes => Set<Tripulante>();
    public DbSet<Reserva> Reservas => Set<Reserva>();
    public DbSet<Aeronave> Aeronaves => Set<Aeronave>();
}