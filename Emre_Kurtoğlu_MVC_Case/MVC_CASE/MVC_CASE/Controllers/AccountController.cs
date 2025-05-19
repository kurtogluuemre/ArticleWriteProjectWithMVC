using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_CASE.Models.VMs;
using MVC_CASE.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace MVC_CASE.Controllers
{
    /// <summary>
    /// Kullanıcı kayıt, giriş ve çıkış işlemlerini yöneten controller sınıfım.
    /// </summary>
    public class AccountController : Controller
    {
        private readonly UserManager<Kullanici> _userManager;
        private readonly SignInManager<Kullanici> _signInManager;

        /// <summary>
        /// Kullanıcı yönetimi ve oturum işlemleri için bağımlılıkları alır.
        /// </summary>
        public AccountController(UserManager<Kullanici> userManager, SignInManager<Kullanici> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Register sayfasını getirir.
        /// </summary>
        [HttpGet]
        public IActionResult Kayit() => View();

        /// <summary>
        /// Kayıt işlemini gerçekleştirir.
        /// </summary>
        /// <param name="model">Kullanıcının kayıt bilgileri.</param>
        [HttpPost]
        public async Task<IActionResult> Kayit(KayitVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new Kullanici { UserName = model.Email, Email = model.Email, FullName = model.FullName };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Giris");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }
            
        }

        /// <summary>
        /// Login sayfasını getirir.
        /// </summary>
        [HttpGet]
        public IActionResult Giris() => View();

        /// <summary>
        /// Login işlemini gerçekleştirir.
        /// </summary>
        /// <param name="model">Kullanıcının giriş bilgileri.</param>
        [HttpPost]
        public async Task<IActionResult> Giris(GirisVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (result.Succeeded)
                return RedirectToAction("Index", "Makale");
            else
            {
                ModelState.AddModelError(string.Empty, "Geçersiz giriş denemesi!");

                return View(model);
            }

        }

        /// <summary>
        /// Logout işlemini gerçekleştirir.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cikis()
        {
            await _signInManager.SignOutAsync(); 
            return RedirectToAction("Index", "Home");
        }
    }
}
