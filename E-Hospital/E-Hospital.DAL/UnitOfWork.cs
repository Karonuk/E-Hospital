using E_Hospital.DAL.Repositories.Abstraction;
using E_Hospital.DAL.Repositories.Implementation;
using System.Data.Entity;

namespace E_Hospital.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new EfRepository<T>(_context);
        }

        private readonly DbContext _context;
    }
}