using E_Hospital.DAL.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace E_Hospital.DAL.Repositories.Implementation
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public EfRepository(EfContext context)
        {
            _context = context;
        }
        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<TEntity> Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate = null, params System.Linq.Expressions.Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = predicate != null 
                ? _context.Set<TEntity>().Where(predicate) 
                : _context.Set<TEntity>();
            
            return includes
                .Aggregate(query, (current, include) => current.Include(include))
                .AsEnumerable();
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        private readonly EfContext _context;
    }
}
