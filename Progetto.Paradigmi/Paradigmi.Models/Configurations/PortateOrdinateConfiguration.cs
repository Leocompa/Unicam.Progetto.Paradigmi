using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paradigmi.Models.Entities;

namespace Paradigmi.Models.Configurations;

public class PortateOrdinateConfiguration : IEntityTypeConfiguration<PortataOrdinata>
{
    public void Configure(EntityTypeBuilder<PortataOrdinata> builder)
    {
        builder.ToTable("PortateOrdinate");
        builder.HasKey(pq => new { pq.OrdinazioneId, PortataId = pq.PortataNome });
        builder.Property(pq => pq.Quantita).IsRequired();

        builder.HasOne(po => po.Ordine)
            .WithMany(o => o.PortateSelezionate)
            .HasForeignKey(po => po.OrdinazioneId);

        builder.HasOne(po => po.Portata)
            .WithMany()
            .HasForeignKey(po => po.PortataNome);
    }
}