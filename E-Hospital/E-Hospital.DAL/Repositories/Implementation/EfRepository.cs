using E_Hospital.DAL.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace E_Hospital.DAL.Repositories.Implementation
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public EfRepository(DbContext context)
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

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = predicate != null
                ? _context.Set<TEntity>().Where(predicate)
                : _context.Set<TEntity>();

            return includes
                .Aggregate(query, (current, include) => current.Include(include))
                .AsEnumerable();
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().Where(predicate);

            return includes
                .Aggregate(query, (current, include) => current.Include(include))
                .FirstOrDefault();
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        private readonly DbContext _context;
    }
}