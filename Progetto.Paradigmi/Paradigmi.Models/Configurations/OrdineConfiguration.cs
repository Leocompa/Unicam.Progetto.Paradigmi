using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paradigmi.Models.Entities;

namespace Paradigmi.Models.Configurations;

public class OrdinazioneConfiguration : IEntityTypeConfiguration<Ordine>
{
    public void Configure(EntityTypeBuilder<Ordine> builder)
    {
        builder.ToTable("Ordini");
        builder.HasKey(o => o.NumeroOrdine);
        builder.Property(o => o.NumeroOrdine).ValueGeneratedOnAdd();
        
        builder.Property(o => o.DataOrdine).IsRequired();
        builder.HasOne(o => o.Utente)
            .WithMany()
            .HasForeignKey(o => o.ClienteEmail);
        
        builder.HasMany(o => o.PortateSelezionate)
            .WithOne(ordinata => ordinata.Ordine)
            .HasForeignKey(pq => pq.OrdinazioneId);
        
        builder.OwnsOne(o => o.IndirizzoConsegna, a =>
        {
            a.Property(a => a.Citta).IsRequired().HasMaxLength(100);
            a.Property(a => a.Cap).IsRequired().HasMaxLength(10);
            a.Property(a => a.Via).IsRequired().HasMaxLength(200);
            a.Property(a => a.Civico).IsRequired().HasMaxLength(10);
        });
    }
}
