using FruitApi.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FruitApi.DataAccess.Repository
{
    public interface IRepository<T> where T : Entity
    {
        Task<IEnumerable<T>> FindAllAsync(params Expression<Func<T, object>>[] includes);
        Task<T> CreateAync(T item);
        Task<T> GetByIdAsync(long id, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> FindAllAsync();
        Task<T> GetByIdAsync(long id);
        Task DeleteAsync(T item);
        Task UpdateAsync(T item);
    }
}
