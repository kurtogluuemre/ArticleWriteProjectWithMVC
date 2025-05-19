using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_CASE.Contexts;
using MVC_CASE.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_CASE.Controllers
{
    /// <summary>
    /// Anasayfa i�lemlerini y�neten controller.
    /// Makaleleri listeleme i�lemini burada yapar.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Anasayfa i�in makaleleri kategorileriyle birlikte listeleyen actionum.
        /// </summary>
        public IActionResult Index()
        {
            var makaleler = _context.Makaleler
                .Include(m => m.MakaleKategoriler)
                    .ThenInclude(mk => mk.Category)
                .ToList(); // T�m makaleleri kategorileriyle beraber getirir

            return View(makaleler); // makale listesini yolluyorum
        }
    }
}
