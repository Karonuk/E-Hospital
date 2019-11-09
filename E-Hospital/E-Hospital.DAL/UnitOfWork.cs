using E_Hospital.DAL.Repositories.Abstraction;
using E_Hospital.DAL.Repositories.Implementation;

namespace E_Hospital.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<T> GetRepository<T>() where T : class
        {
            return new EfRepository<T>(new EfContext());
        }
    }
}