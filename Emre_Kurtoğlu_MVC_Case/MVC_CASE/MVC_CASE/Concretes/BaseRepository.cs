using MVC_CASE.Abstracts;
using System.Linq.Expressions;
using System;
using MVC_CASE.Contracts;
using MVC_CASE.Contexts;
using Microsoft.EntityFrameworkCore;

namespace MVC_CASE.Concretes
{
    /// <summary>
    /// Generic repository sınıfı veritabanı işlemlerimi bu sınıf sağlar.
    /// </summary>
    /// <typeparam name="T">Veritabanımdaki tablolardan birini temsil eden entity tipi.</typeparam>
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {

        protected readonly AppDbContext _context; // Veritabanı işlemleri için kullandığım nesnem
        // Protected yapmamın sebebi miras alan sınıflarımda kullanabilsin 


        /// <summary>
        /// Constructorım parametre atayarak DbContext bağımlılığı enjekte edioyrum.
        /// </summary>
        protected BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gelen entityimi veritabanına ekler.
        /// </summary>
        /// <param name="entity">Eklediğim entityim.</param>
        public void Add(T entity)
        {
            try
            {
                _context.Set<T>().Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Kayıt eklenirken hata oluştu.", ex);
            }
        }

        /// <summary>
        /// Gelen entityimi veritabanından siler.İsteğime bağlı soft delete yapar.
        /// </summary>
        /// <param name="entity">Silinecek entityim.</param>
        /// <param name="softDelete">Soft delete mi yapılacak yoksa komple silme mi ? </param>
        public void Delete(T entity, bool softDelete = true)
        {
            try
            {
                if (softDelete)
                {
                    entity.DeletedDate = DateTime.Now;
                    entity.Status = Enums.Status.Deleted;
                    _context.Set<T>().Update(entity);
                }
                else
                {
                    _context.Set<T>().Remove(entity);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Kayıt silinirken hata oluştu.", ex);
            }
        }

        /// <summary>
        /// Veritabanındaki tüm verileri getirir.
        /// </summary>
        public IQueryable<T> GetAll(bool track = true) => track ? _context.Set<T>() : _context.Set<T>().AsNoTracking();

        /// <summary>
        /// Belirtilen koşula uyan verileri getirir.
        /// </summary>
        /// <param name="condition">Filtreleme koşulu.</param>
        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> condition, bool track = true) =>
            track ?
            _context.Set<T>().Where(condition) :
            _context.Set<T>().Where(condition).AsNoTracking();

        /// <summary>
        /// Verilen id ye sahip veriyi getirir.
        /// </summary>
        /// <param name="id">Veri id değeri.</param>
        public T? GetById(int id) => _context.Set<T>().Find(id); // Find metoduyla direkt primary key üzerinden veri buluyorum.

        /// <summary>
        /// Var olan bir verimi güncellediğim metot.
        /// Update tarihi ve status enumu otomatik olarak değiştirilir.
        /// </summary>
        /// <param name="entity">Güncellenecek entityim.</param>
        public void Update(T entity)
        {
            try
            {
                entity.UpdatedDate = DateTime.Now;
                entity.Status = Enums.Status.Updated;
                _context.Set<T>().Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Kayıt güncellenirken hata oluştu.", ex);
            }

        }
    }
}
