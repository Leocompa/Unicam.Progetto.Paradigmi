using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paradigmi.Models.Entities;

namespace Paradigmi.Models.Configurations;

   public class PortataConfiguration : IEntityTypeConfiguration<Portata>
    {
        public void Configure(EntityTypeBuilder<Portata> builder)
        {
            builder.ToTable("Portate");
            builder.HasKey(p => p.Nome);
            builder.Property(p => p.Prezzo).IsRequired();
            builder.Property(p => p.Tipo).IsRequired();
        }
}