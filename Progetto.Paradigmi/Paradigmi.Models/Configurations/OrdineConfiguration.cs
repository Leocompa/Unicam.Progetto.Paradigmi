using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paradigmi.Models.Entities;

namespace Paradigmi.Models.Configurations;

public class OrdineConfiguration : IEntityTypeConfiguration<Ordine>
{
    public void Configure(EntityTypeBuilder<Ordine> builder)
    {
        builder.ToTable("Ordini");
        
        //builder.Property(p => p.IndirizzoConsegna).HasMaxLength(100);
        /*
        builder.HasMany(ordine => ordine.Portate)
            .WithOne(ordinata => ordinata.Ordine)
            .HasForeignKey(ordinata =>  ordinata.Ordine);
        */
        
        // Imposta la chiave primaria e specifica che è generata automaticamente dal database
        builder.HasKey(p => p.NOrdine)
            .HasName("PK_Ordini_NumeroOrdine")
            .IsClustered();

        builder.Property(p => p.NOrdine)
            .HasColumnName("NOrdine")
            .ValueGeneratedOnAdd(); // Specifica che il valore viene generato automaticamente dal database

        builder.Property(p => p.DataOrdine).HasMaxLength(100);
        // builder.Property(p => p.IndirizzoConsegna).HasMaxLength(100);
        
        
        builder.ToTable("OrdiniPortate"); // Tabella di join
        builder.HasMany<Portata>(o => o.Portate.Values)
            .WithOne()
            .HasForeignKey("NOrdine") // Chiave esterna che fa riferimento all'ordine
            .IsRequired();
    }
}