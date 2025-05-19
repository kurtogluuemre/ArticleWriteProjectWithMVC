using Microsoft.AspNetCore.Identity;

namespace MVC_CASE.Models
{
    /// <summary>
    /// Uygulamada kimlik doğrulama ve kullanıcı yönetimi için kullanılan kullanıcı sınıfı.
    /// IdentityUser sınıfından türetilmiştir.
    /// </summary>
    public class Kullanici : IdentityUser
    {
        private string _fullName;
        private ICollection<Makale> _makaleler;

        /// <summary>
        /// Kullanıcının tam adını encapsulationla tutan property.
        /// </summary>
        public string FullName
        {
            get => _fullName;
            set => _fullName = value;
        }

        /// <summary>
        /// Kullanıcının yazdığı makaleleri tutan koleksiyon.
        /// Bir kullanıcı birden fazla makale yazabilir.
        /// </summary>
        public virtual ICollection<Makale> Makaleler
        {
            get => _makaleler;
            set => _makaleler = value;
        }
    }
}
