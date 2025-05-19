using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MVC_CASE.Models;

namespace MVC_CASE.Configs
{
    /// <summary>
    /// Category entity'si için konfigürasyonları tanımlar.
    /// </summary>
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(x => x.CreatedDate).HasColumnType("datetime");

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            
            builder.HasData(
                new Category { Id = 1, Name = "Teknoloji", CreatedDate = new DateTime(2000,01,1)},
                new Category { Id = 2, Name = "Eğitim", CreatedDate = new DateTime(2000, 01, 1) },
                new Category { Id = 3, Name = "Sağlık", CreatedDate = new DateTime(2000, 01, 1) }
            );
        }
    }
}
