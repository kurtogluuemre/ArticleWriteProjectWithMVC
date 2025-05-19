using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MVC_CASE.MyValidations;

namespace MVC_CASE.Models.VMs
{
    /// <summary>
    /// Register işlemi için gerekli bilgileri taşıyan ViewModel.
    /// </summary>
    public class KayitVM
    {
        /// <summary>
        /// Ad ve soyad bilgisi.
        /// </summary>
        [Required(ErrorMessage = "Ad alanı zorunludur!")]
        [DisplayName("Ad Soyad")]
        [RakamOlmasin(ErrorMessage = "Tam ad rakam içermemelidir!")] // Bu benim yazdığım validationum
        public string FullName { get; set; }

        /// <summary>
        /// Kullanıcının kayıt için kullanacağı e-posta adresi.
        /// </summary>
        [Required(ErrorMessage = "Email alanı zorunludur!")]
        [EmailAddress(ErrorMessage = "Geçerli formatta girmediniz!")]
        [DisplayName("E-Posta")]
        [SifreGuvenlik(ErrorMessage = "Eposta en az bir tane rakam veya özel karakter içermelidir")] // Bu benim yazdığım validationum
        public string Email { get; set; }

        /// <summary>
        /// Kullanıcının kayıt için belirlediği şifre.
        /// </summary>
        [Required(ErrorMessage = "Şifre alanı zorunludur!")]
        [DataType(DataType.Password)]
        [SifreGuvenlik(ErrorMessage = "Şifre en az bir tane rakam veya özel karakter içermelidir")] // Bu benim yazdığım validationum
        public string Password { get; set; }

        /// <summary>
        /// Kullanıcının şifresini doğrulamak için tekrar girdiği şifre.
        /// </summary>
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor!")]
        public string ConfirmPassword { get; set; }
    }
}
