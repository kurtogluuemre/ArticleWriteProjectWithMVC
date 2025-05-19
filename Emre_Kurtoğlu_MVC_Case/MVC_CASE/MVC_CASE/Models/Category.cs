using MVC_CASE.Abstracts;

namespace MVC_CASE.Models
{
    /// <summary>
    /// Makalelerin ait olduğu kategori bilgisini temsil eden sınıf.
    /// </summary>
    public class Category : BaseEntity
    {
        private string _name;
        private ICollection<MakaleKategori> _makaleKategoriler;

        public Category()
        {
            CreatedDate = DateTime.Now;
            _makaleKategoriler = new List<MakaleKategori>(); // Varsayılan olarak oluşturulma tarihini şu anki zamana ayarlar ve MakaleKategoriler listesini başlatır.
        }

        /// <summary>
        /// Kategorinin adını encapsulationla alır.
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        /// <summary>
        /// Kategoriye ait makale-kategori ilişkilerini tutan koleksiyon.
        /// Bir kategori birçok makaleye ait olabilir.
        /// </summary>
        public virtual ICollection<MakaleKategori> MakaleKategoriler
        {
            get => _makaleKategoriler;
            set => _makaleKategoriler = value;
        }
    }
}
