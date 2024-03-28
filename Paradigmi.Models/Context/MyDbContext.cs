using Microsoft.EntityFrameworkCore;
using Paradigmi.Models.Entities;

namespace Paradigmi.Models.Context;

public class MyDbContext : DbContext
{
    public MyDbContext() : base()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> config) : base(config)
    {
    }

    public DbSet<Utente> Utenti { get; set; }
    public DbSet<Ordine> Ordini { get; set; }
    public DbSet<Portata> Portate { get; set; }
    public DbSet<PortataOrdinata> PortateOrdinate { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }
}