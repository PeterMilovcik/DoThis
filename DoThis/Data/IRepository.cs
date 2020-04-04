using System.Collections.Generic;
using System.Threading.Tasks;
using Beeffective.Data.Entities;

namespace Beeffective.Data
{
    public interface IRepository<T> where T : Entity
    {
        Task<List<T>> LoadAsync();
        Task SaveAsync();
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task RemoveAsync(T entity);
    }
}