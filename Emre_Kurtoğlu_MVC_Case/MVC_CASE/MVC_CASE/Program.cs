using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC_CASE.Concretes;
using MVC_CASE.Contexts;
using MVC_CASE.Contracts;
using MVC_CASE.Models;
using MVC_CASE.UnitOfWorks;

namespace MVC_CASE
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args); 

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // DbContext servisini ekleyip ve SQL Server kullan�yorum
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConn")));

            // Identity servisi ekleyip �ifre kurallar�n� belirliyorum
            builder.Services.AddIdentity<Kullanici, IdentityRole>(x => x.Password = new PasswordOptions()
            {
                RequireDigit = false, // Rakam gereksinimi yok
                RequiredLength = 1, // Minimum karakter uzunlu�u 1
                RequireLowercase = false, // K���k harf zorunlulu�u yok
                RequireUppercase = false, // B�y�k harf zorunlulu�u yok
                RequireNonAlphanumeric = false, // �zel karakter zorunlulu�u yok
                RequiredUniqueChars = 0, // Tekil karakter zorunlulu�u yok
            }).AddEntityFrameworkStores<AppDbContext>() // Identity i�in EF Core kullan�yorum.
              .AddDefaultTokenProviders();

            // Repository katman� i�in dependency injection ayarlar�n� yap�yorum
            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddScoped<IManagerRepository, ManagerRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
