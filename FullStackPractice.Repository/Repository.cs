using FullStackPractice.Persistence;
using FullStackPractice.Persistence.Models;
using FullStackPractice.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FullStackPractice.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly FullStackPracticeDbContext _dbcontext;

        public Repository(FullStackPracticeDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task Add(T entity)
        {
            await _dbcontext.Set<T>().AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<T> entities)
        {
            await _dbcontext.Set<T>().AddRangeAsync(entities);
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression)
        {
            return await _dbcontext.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbcontext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbcontext.Set<T>().FindAsync(id);
        }

        public async Task<T> Update(T entity)
        {
            return _dbcontext.Update(entity).Entity;
        }

        public async Task Remove(T entity)
        {
            _dbcontext.Set<T>().Remove(entity);
        }

        public async Task RemoveRange(IEnumerable<T> entities)
        {
            _dbcontext.Set<T>().RemoveRange(entities);
        }
    }
    
}
