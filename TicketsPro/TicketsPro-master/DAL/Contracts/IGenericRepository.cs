using System;
using System.Collections.Generic;
namespace DAL.Contracts
{

    public interface IGenericRepository<T, TKey>
    {
        T GetById(TKey id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
