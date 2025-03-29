using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly FUMiniHotelManagementContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(FUMiniHotelManagementContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression).ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public virtual void Update(T entity)
        {
            // Detach any existing entity with the same key to avoid tracking conflicts
            var keyValues = _context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties
                .Select(p => p.PropertyInfo.GetValue(entity)).ToArray();

            var existingEntity = _dbSet.Find(keyValues);

            if (existingEntity != null)
            {
                _context.Entry(existingEntity).State = EntityState.Detached;
            }

            // Now attach and mark as modified
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}