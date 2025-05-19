using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC_CASE.Configs;
using MVC_CASE.Models;
using System.Reflection.Emit;

namespace MVC_CASE.Contexts
{
    /// <summary>
    /// Uygulamanın veritabanı bağlantısını ve DbSet tanımlarını içeren context sınıfıdır.
    /// IdentityDbContext sınıfından türetilmiştir ve Kullanici entity'siyle kimlik yönetimi sağlar.
    /// </summary>
    public class AppDbContext : IdentityDbContext<Kullanici>
    {
        /// <summary>
        /// Kategorileri temsil eden DbSet.
        /// </summary>
        public DbSet<Category> Categories { get; set; }
        /// <summary>
        /// Makaleleri temsil eden DbSet.
        /// </summary>
        public DbSet<Makale> Makaleler { get; set; }
        /// <summary>
        /// Makale-Kategori ilişkisini temsil eden DbSet.
        /// </summary>
        public DbSet<MakaleKategori> MakaleKategoriler { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /// <summary>
            /// FluentApi kullanarak yaptığımız konfigürasyonları metot olarak çağırıyoruz kodda kalabalık yapmasını istemediğimiz için farklı
            /// birer class olarak tanımladık yoksa burda da modelBuilder kullanarak yapabilirdik.
            /// </summary>
            modelBuilder.ApplyConfiguration(new MakaleConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new MakaleCategoryConfiguration());
        }
    }
}
