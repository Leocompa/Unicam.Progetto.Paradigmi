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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {                
           optionsBuilder.UseSqlServer( "Server=localhost;Database=ProgettoParadigmi;User Id=paradigmi;Password=unicamParadigmi!;Encrypt=False;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>()
            .HasKey(a => a.AddressId);
        
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }
}