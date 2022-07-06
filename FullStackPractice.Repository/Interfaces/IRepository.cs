using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FullStackPractice.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();

        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression);

        Task Add(T Entity);
        
        Task AddRange(IEnumerable<T> entities);

        Task<T> Update(T Entity);

        Task Remove(T entity);

        Task RemoveRange(IEnumerable<T> entities);
    }
}
