using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paradigmi.Models.Entities;

namespace Paradigmi.Models.Configurations;

   public class OrdineConfiguration : IEntityTypeConfiguration<Ordine>
    {
        public void Configure(EntityTypeBuilder<Ordine> builder)
        {
            builder.ToTable("Ordini");
            builder.HasKey(p => p.NumeroOrdine);
            //builder.Property(p => p.Utente)
             //   .HasMaxLength(100);
            builder.Property(p => p.DataOrdine).HasMaxLength(100);
          //  builder.Property(p => p.IndirizzoConsegna);
          /*
          builder.HasMany(ordine => ordine.Portate)
              .WithOne(ordinata => ordinata.Ordine)
              .HasForeignKey(ordinata =>  ordinata.Ordine);
*/

        }
}