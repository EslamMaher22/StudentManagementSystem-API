using Day2.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Day2.Repository
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        public ITIContext db;
        public GenericRepository(ITIContext db)
        {
            this.db = db;
        }
        public List<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = db.Set<TEntity>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.ToList();
        }

        public TEntity GetById(int id)
        {
            return db.Set<TEntity>().Find(id);
        }

        public List<TEntity> Find(Expression<Func<TEntity, bool>> predicate, string[]? includes = null)
        {
            IQueryable<TEntity> query = db.Set<TEntity>();
            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }
            return query.Where(predicate).ToList();
        }

        public void Add(TEntity entity)
        {
            db.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            TEntity t = GetById(id);
            db.Set<TEntity>().Remove(t);
        }
    }
}
