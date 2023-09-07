using FruitApi.DataAccess.Models;
using FruitApi.DataAccess.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FruitApi.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly FruitContext _fruitContext;
        private DbSet<T> dataset;

        public Repository(FruitContext fruitContext)
        {
            _fruitContext = fruitContext;
            dataset = _fruitContext.Set<T>();
        }

        public async Task<T> CreateAync(T item)
        {
            await dataset.AddAsync(item);
            await _fruitContext.SaveChangesAsync();
            return item;
        }

        public async Task<IEnumerable<T>> FindAllAsync() =>  await dataset.ToListAsync();

        public async Task<T> GetByIdAsync(long id) => await dataset.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<T>> FindAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dataset;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(long id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dataset;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task DeleteAsync(T item)
        {
            dataset.Remove(item);
            await _fruitContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T item)
        {
            dataset.Update(item);
            await _fruitContext.SaveChangesAsync();
        }
    }
}
