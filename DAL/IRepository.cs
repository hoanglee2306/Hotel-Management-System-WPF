using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DAL;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    T GetById(int id);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Update(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    void SaveChanges();
}