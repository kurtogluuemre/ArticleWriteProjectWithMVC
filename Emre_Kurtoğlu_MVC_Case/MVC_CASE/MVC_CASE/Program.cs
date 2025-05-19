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

            // DbContext servisini ekleyip ve SQL Server kullanýyorum
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConn")));

            // Identity servisi ekleyip þifre kurallarýný belirliyorum
            builder.Services.AddIdentity<Kullanici, IdentityRole>(x => x.Password = new PasswordOptions()
            {
                RequireDigit = false, // Rakam gereksinimi yok
                RequiredLength = 1, // Minimum karakter uzunluðu 1
                RequireLowercase = false, // Küçük harf zorunluluðu yok
                RequireUppercase = false, // Büyük harf zorunluluðu yok
                RequireNonAlphanumeric = false, // Özel karakter zorunluluðu yok
                RequiredUniqueChars = 0, // Tekil karakter zorunluluðu yok
            }).AddEntityFrameworkStores<AppDbContext>() // Identity için EF Core kullanýyorum.
              .AddDefaultTokenProviders();

            // Repository katmaný için dependency injection ayarlarýný yapýyorum
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
