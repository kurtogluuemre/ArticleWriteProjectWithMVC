using MVC_CASE.Abstracts;

namespace MVC_CASE.Models
{
    /// <summary>
    /// Uygulamada bir makaleyi temsil eden sınıf.
    ///
    public class Makale : BaseEntity
    {
        private string _baslik;
        private string _icerik;
        private string _kullaniciId;
        private Kullanici _kullanici;
        private ICollection<MakaleKategori> _makaleKategoriler;

        /// <summary>
        /// Makalenin başlığını tutan property.
        /// </summary>
        public string Baslik
        {
            get => _baslik;
            set => _baslik = value;
        }

        /// <summary>
        /// Makalenin içeriğini tutan property.
        /// </summary>
        public string Icerik
        {
            get => _icerik;
            set => _icerik = value;
        }

        /// <summary>
        /// Makaleyi yazan kullanıcının Id bilgisi.
        /// </summary>
        public string KullaniciId
        {
            get => _kullaniciId;
            set => _kullaniciId = value;
        }

        /// <summary>
        /// Makaleyi yazan kullanıcı ile ilişki navigation property.
        /// </summary>
        public virtual Kullanici Kullanici
        {
            get => _kullanici;
            set => _kullanici = value;
        }

        /// <summary>
        /// Makalenin ait olduğu kategorilerle ilişkisini tutan koleksiyon.
        /// </summary>
        public virtual ICollection<MakaleKategori> MakaleKategoriler
        {
            get => _makaleKategoriler;
            set => _makaleKategoriler = value;
        }
    }
}
