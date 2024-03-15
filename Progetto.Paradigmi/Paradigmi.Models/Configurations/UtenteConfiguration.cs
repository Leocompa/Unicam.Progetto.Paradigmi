using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paradigmi.Models.Entities;

namespace Paradigmi.Models.Configurations;

   public class UtenteConfiguration : IEntityTypeConfiguration<Utente>
    {
        public void Configure(EntityTypeBuilder<Utente> builder)
        {
            builder.ToTable("Utenti");
            builder.HasKey(p => p.Email);
            builder.Property(p => p.Nome)
                .HasMaxLength(100);
            builder.Property(p => p.Cognome).HasMaxLength(100);
            builder.Property(p => p.Ruolo).HasConversion(ruolo=>ruolo.ToString(),
                ruolo=>RuoloExtensions.asRuolo(ruolo)
                ).IsRequired();
            builder.Property(p => p.Password).HasMaxLength(20);
        }
}