using E_Hospital.DAL.Repositories.Abstraction;

namespace E_Hospital.DAL
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class;
    }
}