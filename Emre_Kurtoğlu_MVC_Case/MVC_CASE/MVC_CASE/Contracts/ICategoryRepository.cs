using MVC_CASE.Models;

namespace MVC_CASE.Contracts
{
    /// <summary>
    /// Generic repositoryim zaten IBaseRepository Temel CRUD işlemlerimi o sağladığı için ekstra metot eklemiyorum
    /// </summary>
    public interface ICategoryRepository : IBaseRepository<Category>
    {
    }
}
