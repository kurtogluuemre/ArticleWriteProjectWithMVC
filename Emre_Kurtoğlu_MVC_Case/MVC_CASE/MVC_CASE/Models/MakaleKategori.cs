using MVC_CASE.Abstracts;

namespace MVC_CASE.Models
{
    /// <summary>
    /// Makale ve kategori arasındaki çoka-çok ilişkiyi temsil eden ara tablo modeli.
    /// </summary>
    public class MakaleKategori : BaseEntity
    {
        private int _makaleId;
        private Makale _makale;
        private int _categoryId;
        private Category _category;

        /// <summary>
        /// İlişkili makalenin Id'si.
        /// </summary>
        public int MakaleId // Fk
        {
            get => _makaleId;
            set => _makaleId = value;
        }

        /// <summary>
        /// Makale ile olan navigation property.
        /// </summary>
        public virtual Makale Makale
        {
            get => _makale;
            set => _makale = value;
        }

        /// <summary>
        /// İlişkili kategorinin Id'si.
        /// </summary>
        public int CategoryId // Fk
        {
            get => _categoryId;
            set => _categoryId = value;
        }

        /// <summary>
        /// Kategori ile olan navigation property.
        /// </summary>
        public virtual Category Category
        {
            get => _category;
            set => _category = value;
        }
    }
}
