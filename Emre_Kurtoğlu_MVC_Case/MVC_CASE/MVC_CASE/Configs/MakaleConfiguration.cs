using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC_CASE.Models;

namespace MVC_CASE.Configs
{
    /// <summary>
    /// Makale entity'si için konfigürasyonları tanımlar.
    /// </summary>
    public class MakaleConfiguration : IEntityTypeConfiguration<Makale>
    {
        public void Configure(EntityTypeBuilder<Makale> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Baslik)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(a => a.Icerik)
                .IsRequired();

            builder.Property(x => x.CreatedDate).HasColumnType("datetime");


            builder.HasOne(a => a.Kullanici)
                .WithMany(u => u.Makaleler)
                .HasForeignKey(a => a.KullaniciId);
        }
    }
}
