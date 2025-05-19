using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_CASE.Models;
using MVC_CASE.Enums;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_CASE.Models.VMs;
using MVC_CASE.Contexts;

namespace MVC_CASE.Controllers
{
    /// <summary>
    /// Makaleler ile ilgili işlemleri yöneten controller.
    /// Sadece giriş yapmış kullanıcılar erişebilir.
    /// </summary>
    [Authorize]
    public class MakaleController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Kullanici> _userManager;

        public MakaleController(AppDbContext context, UserManager<Kullanici> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Kullanıcının silinmemiş makalelerini listeler.
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var makaleler = await _context.Makaleler
                .Where(m => m.KullaniciId == user.Id && m.Status != Status.Deleted)
                .Include(m => m.MakaleKategoriler)
                .ThenInclude(mk => mk.Category)
                .ToListAsync();

            return View(makaleler);
        }

        /// <summary>
        /// Yeni makale oluşturmak için boş formu döner.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _context.Categories.ToListAsync();
            var viewModel = new MakaleViewModel
            {
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList()
            };
            return View(viewModel);
        }
        /// <summary>
        /// Yeni makale oluşturma işlemini gerçekleştirir.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MakaleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _context.Categories.ToListAsync();
                model.Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);

            var makale = new Makale
            {
                Baslik = model.Title,
                Icerik = model.Content,
                KullaniciId = user.Id,
                Status = Status.Created
            };

            _context.Makaleler.Add(makale);
            await _context.SaveChangesAsync();

            foreach (var categoryId in model.SelectedCategoryIds)
            {
                _context.MakaleKategoriler.Add(new MakaleKategori
                {
                    MakaleId = makale.Id,
                    CategoryId = categoryId
                });
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        /// <summary>
        /// Mevcut bir makaleyi düzenlemek için formu doldurur.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var makale = await _context.Makaleler
                .Include(m => m.MakaleKategoriler)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (makale == null)
            {
                return NotFound();
            }

            var viewModel = new MakaleViewModel
            {
                Id = makale.Id, 
                Title = makale.Baslik,
                Content = makale.Icerik,
                SelectedCategoryIds = makale.MakaleKategoriler.Select(mk => mk.CategoryId).ToList(),
                Categories = _context.Categories
                    .Select(c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    }).ToList()
            };

            return View(viewModel);
        }

        /// <summary>
        /// Makale düzenleme işlemini gerçekleştirir.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MakaleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                
                model.Categories = _context.Categories
                    .Select(c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    }).ToList();

                return View(model);
            }

            var makale = await _context.Makaleler
                .Include(m => m.MakaleKategoriler)
                .FirstOrDefaultAsync(m => m.Id == model.Id);

            if (makale == null)
            {
                return NotFound();
            }
            // makale güncelleme işlmeleri
            makale.Baslik = model.Title;
            makale.Icerik = model.Content;
            makale.UpdatedDate = DateTime.Now;
            makale.Status = Status.Updated;

            // eski kategorileri temizle
            _context.MakaleKategoriler.RemoveRange(makale.MakaleKategoriler);

            // yeni seçilen kategorileri ekle
            makale.MakaleKategoriler = model.SelectedCategoryIds
                .Select(catId => new MakaleKategori
                {
                    CategoryId = catId,
                    MakaleId = makale.Id
                }).ToList();

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Makaleyi Soft Delete ile siler.
        /// </summary>
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var makale = await _context.Makaleler.FirstOrDefaultAsync(m => m.Id == id && m.KullaniciId == user.Id && m.Status != Status.Deleted);

            if (makale == null) return NotFound();

            //veritabanından silinmiyor, durum güncelleniyor
            makale.DeletedDate = DateTime.Now;
            makale.Status = Status.Deleted;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
