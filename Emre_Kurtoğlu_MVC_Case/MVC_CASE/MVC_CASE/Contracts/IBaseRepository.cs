using MVC_CASE.Abstracts;
using System.Linq.Expressions;

namespace MVC_CASE.Contracts
{
    /// <summary>
    /// Generic repositorynin interfacei.
    /// Temel CRUD işlemlerimi bu metotlar sağlıyor
    /// </summary>
    /// <typeparam name="T">Üzeinde işlem yaptığım entity tipi.</typeparam>
    public interface IBaseRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Veritabanındaki tüm verileri getirir.
        /// </summary>
        IQueryable<T> GetAll(bool track = true);


        /// <summary>
        /// Belirtilen koşula uyan verileri getirir.
        /// </summary>
        /// <param name="condition">Filtreleme koşulu.</param>
        IQueryable<T> GetByCondition(Expression<Func<T, bool>> condition, bool track = true);


        /// <summary>
        /// Verilen id ye sahip veriyi getirir.
        /// </summary>
        /// <param name="id">Veri id değeri.</param>
        T? GetById(int id);


        /// <summary>
        /// Gelen entityimi veritabanına ekler.
        /// </summary>
        /// <param name="entity">Eklenecek varlık.</param>
        void Add(T entity);


        /// <summary>
        /// Var olan bir verimi güncellediğim metot.
        /// Update tarihi ve status enumu otomatik olarak değiştirilir.
        /// </summary>
        /// <param name="entity">Güncellenecek entityim.</param>
        void Update(T entity);


        /// <summary>
        /// Gelen entityimi veritabanından siler.İsteğime bağlı soft delete yapar.
        /// </summary>
        /// <param name="entity">Silinecek entityim.</param>
        /// <param name="softDelete">Soft delete mi yapılacak yoksa komple silme mi ? </param>
        void Delete(T entity, bool softDelete = true);
    }
}
