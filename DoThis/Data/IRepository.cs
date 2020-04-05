using System.Collections.Generic;
using System.Threading.Tasks;
using Beeffective.Data.Entities;

namespace Beeffective.Data
{
    public interface IRepository<T> where T : Entity
    {
        Task<List<T>> LoadAsync();
        Task SaveAsync();
        T Add(T newEntity);
        IEnumerable<T> Add(IEnumerable<T> newEntities);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        void Remove(T entity);
        Task RemoveAsync(T entity);
        void RemoveAll();
    }
}