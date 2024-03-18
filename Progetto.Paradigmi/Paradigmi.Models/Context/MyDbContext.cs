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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {                
           optionsBuilder.UseSqlServer( "Server=localhost;Database=ProgettoParadigmi;User Id=paradigmi;Password=unicamParadigmi1!;Encrypt=False;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /*
        modelBuilder.Entity<Address>()
            .HasKey(a => a.AddressId);
        
        modelBuilder.Entity<PortataOrdinata>().Property(p => p.Piatto)
            .HasConversion(p => p.Nome,
                nome=> GetPortataByNome(nome)
            );
        
        
        modelBuilder.Entity<Ordine>().Property(p => p.Utente).HasConversion(utente => utente.Email,
            email => Utenti.FirstOrDefault(utente => utente.Email == email));
            */
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
        
    }
}