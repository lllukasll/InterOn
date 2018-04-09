using InterOn.Data.DbModels;
using InterOn.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InterOn.Repo.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DataContext _context;
        private readonly DbSet<T> _entities;

        public Repository(DataContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _entities;
        }
        public bool Exist(Expression<Func<T, bool>> match)
        {
            return _context.Set<T>().Any(match);
        }

        public T Get(long id)
        {
            return _entities.SingleOrDefault(s => s.Id == id);
        }

        public void Insert(T entity)
        {
            if(entity == null)
                throw new ArgumentNullException("entity");

            _entities.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            if(entity == null)
                throw new ArgumentNullException("entity");

            _context.SaveChanges();
        }

        public void Remove(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            _entities.Remove(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        public virtual async Task<ICollection<T>> GetAllAsyn()
        {

            return await _entities.ToListAsync();
        }

        public virtual T Get(int id)
        {
            return _entities.Find(id);
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await _entities.FindAsync(id);
        }

        public virtual T Add(T t)
        {

            _entities.Add(t);
            _context.SaveChanges();
            return t;
        }

        public virtual async Task<T> AddAsyn(T t)
        {
            _entities.Add(t);
            await _context.SaveChangesAsync();
            return t;

        }

        public virtual T Find(Expression<Func<T, bool>> match)
        {
            return _entities.SingleOrDefault(match);
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await _entities.SingleOrDefaultAsync(match);
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return _entities.Where(match).ToList();
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await _entities.Where(match).ToListAsync();
        }

        public virtual void Delete(T entity)
        {
            _entities.Remove(entity);
            _context.SaveChanges();
        }

        public virtual async Task<int> DeleteAsyn(T entity)
        {
            _entities.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public virtual T Update(T t, object key)
        {
            if (t == null)
                return null;
            var exist = _entities.Find(key);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(t);
                _context.SaveChanges();
            }
            return exist;
        }

        public virtual async Task<T> UpdateAsyn(T t, object key)
        {
            if (t == null)
                return null;
            T exist = await _entities.FindAsync(key);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(t);
                await _context.SaveChangesAsync();
            }
            return exist;
        }

        public  virtual async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _entities.Where(predicate);
            return query;
        }

        public virtual async Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate)
        {
            return await _entities.Where(predicate).ToListAsync();
        }

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {

            IQueryable<T> queryable = GetAll();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {

                queryable = queryable.Include<T, object>(includeProperty);
            }

            return queryable;
        }
       
    }
}

