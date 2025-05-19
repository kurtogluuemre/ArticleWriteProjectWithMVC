using MVC_CASE.Concretes;
using MVC_CASE.Contexts;
using MVC_CASE.Contracts;

namespace MVC_CASE.UnitOfWorks
{
    /// <summary>
    /// Repository nesnelerinin yönetimini ve veritabanı işlemlerini gerçekleştiren sınıf.
    /// Repositoryleri Lazy Loading ile oluşturur.
    /// </summary>
    public class ManagerRepository : IManagerRepository
    {

        private readonly AppDbContext _context;

        private readonly Lazy<ICategoryRepository> _CategoryRepository;
        private readonly Lazy<IMakaleRepository> _MakaleRepository;
        private readonly Lazy<IMakaleKategoriRepository> _MakaleKategoriRepository;


        public ManagerRepository(AppDbContext context)
        {
            _context = context;

            // Repositoryler ihtiyaç duyulduğunda oluşturulacak şekilde ayarlanıyor.
            _CategoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(_context));
            _MakaleRepository = new Lazy<IMakaleRepository>(() => new MakaleRepository(_context));
            _MakaleKategoriRepository = new Lazy<IMakaleKategoriRepository>(() => new MakaleKategoriRepository(_context));
        }

        /// <summary>
        /// Kategori repository erişimi sağlar.
        /// </summary>
        public ICategoryRepository CategoryRepository => _CategoryRepository.Value;

        /// <summary>
        /// Makale repository erişimi sağlar.
        /// </summary>
        public IMakaleRepository MakaleRepository => _MakaleRepository.Value;

        /// <summary>
        /// Makale-Kategori repository erişimi sağlar.
        /// </summary>
        public IMakaleKategoriRepository MakaleKategoriRepository => _MakaleKategoriRepository.Value;


        /// <summary>
        /// Yapılan değişiklikleri veritabanına kaydeder.
        /// </summary>
        /// <returns>İşlem başarılıysa true, değilse false döner.</returns>
        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
