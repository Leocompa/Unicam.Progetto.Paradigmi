using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paradigmi.Models.Context;
using Paradigmi.Models.Entities;

namespace Paradigmi.Models.Configurations;

public class PortataOrdinataConfiguration : IEntityTypeConfiguration<PortataOrdinata>
{
    public void Configure(EntityTypeBuilder<PortataOrdinata> builder)
    {
        builder.ToTable("PortateOrdinate");
        builder.HasKey(p => p.id);
        builder.Property(p => p.Piatto);
        builder.Property(p => p.Quantita).IsRequired();
      /*
        builder.HasOne(portataOrdinata => portataOrdinata.Ordine)
            .WithMany(ordine => ordine.Portate)
            .HasForeignKey(ordinata => ordinata.Ordine);
            */
    }

}