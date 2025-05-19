using MVC_CASE.Contracts;

namespace MVC_CASE.UnitOfWorks
{
    /// <summary>
    /// Repository nesnelerinin bir arada yönetilmesini ve tek bir yerden erişilmesini sağlayan yapı.
    /// Ayrıca save changes  işlemini de yönetir.
    /// </summary>
    public interface IManagerRepository
    {
        /// <summary>
        /// Kategori işlemleri için repository erişimi sağlar.
        /// </summary>
        ICategoryRepository CategoryRepository { get; }

        /// <summary>
        /// Makale işlemleri için repository erişimi sağlar.
        /// </summary>
        IMakaleRepository MakaleRepository { get; }

        /// <summary>
        /// Makale-Kategori ilişki işlemleri için repository erişimi sağlar.
        /// </summary>
        IMakaleKategoriRepository MakaleKategoriRepository { get; }

        /// <summary>
        /// Yapılan değişiklikleri veritabanına kaydeder.
        /// </summary>
        /// <returns>İşlem başarılıysa true, değilse false döner.</returns>
        bool Save();
    }
}
