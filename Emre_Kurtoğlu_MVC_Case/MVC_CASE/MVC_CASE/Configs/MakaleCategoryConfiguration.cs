using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MVC_CASE.Models;

namespace MVC_CASE.Configs
{
    /// <summary>
    /// MakaleKategori entity'si için konfigürasyonları tanımlar.
    /// </summary>
    public class MakaleCategoryConfiguration : IEntityTypeConfiguration<MakaleKategori>
    {
        public void Configure(EntityTypeBuilder<MakaleKategori> builder)
        {
            builder.HasKey(ac => new { ac.MakaleId, ac.CategoryId });

            builder.HasOne(ac => ac.Makale)
                .WithMany(a => a.MakaleKategoriler)
                .HasForeignKey(ac => ac.MakaleId);

            builder.HasOne(ac => ac.Category)
                .WithMany(c => c.MakaleKategoriler)
                .HasForeignKey(ac => ac.CategoryId);
        }
    }
}
