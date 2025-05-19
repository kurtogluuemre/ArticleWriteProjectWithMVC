using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MVC_CASE.MyValidations;

namespace MVC_CASE.Models.VMs
{
    /// <summary>
    /// Login işlemi için gerekli bilgileri taşıyan ViewModel.
    /// </summary>
    public class GirisVM
    {
        /// <summary>
        /// Kullanıcının giriş için kullandığı e-posta adresi.
        /// </summary>
        [Required(ErrorMessage = "Email alanı boş geçilemez.")]
        [EmailAddress(ErrorMessage = "Geçerli bir format giriniz.")]
        [DisplayName("Email")]
        [SifreGuvenlik(ErrorMessage = "Eposta en az bir tane rakam veya özel karakter içermelidir")] // Bu benim yazdığım validationum
        public string Email { get; set; }

        /// <summary>
        /// Kullanıcının giriş için kullandığı şifre.
        /// </summary>
        [Required(ErrorMessage = "Şifre alanı boş geçilemez.")]
        [DataType(DataType.Password)]
        [SifreGuvenlik(ErrorMessage = "Şifre en az bir tane rakam veya özel karakter içermelidir")] // Bu benim yazdığım validationum
        public string Password { get; set; }

        /// <summary>
        /// Kullanıcının giriş bilgileri saklı kalıp kalmayacağı belirten seçenek.
        /// </summary>
        [Display(Name = "Beni hatırla")]
        public bool RememberMe { get; set; }
    }
}
