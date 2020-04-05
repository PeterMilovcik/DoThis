using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Beeffective.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Beeffective.Data
{
    public class CellRepository : IRepository<CellEntity>
    {
        public Task<List<CellEntity>> LoadAsync()
        {
            using var context = new CellContext();
            return context.Cells.ToListAsync();
        }

        public async Task SaveAsync()
        {
            await using var context = new CellContext();
            await context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task<CellEntity> AddAsync(CellEntity entity)
        {
            await using var context = new CellContext();
            var entry = await context.AddAsync(entity, CancellationToken.None);
            await context.SaveChangesAsync(CancellationToken.None);
            return entry.Entity;
        }

        public async Task<CellEntity> UpdateAsync(CellEntity entity)
        {
            await using var context = new CellContext();
            var entry = context.Update(entity);
            await context.SaveChangesAsync(CancellationToken.None);
            return entry.Entity;
        }

        public async Task RemoveAsync(CellEntity entity)
        {
            await using var context = new CellContext();
            context.Remove(entity);
            await context.SaveChangesAsync(CancellationToken.None);
        }

        public CellEntity Add(CellEntity newCellEntity)
        {
            using var context = new CellContext();
            if (context.Cells.Contains(newCellEntity)) return null;
            var entry = context.Add(newCellEntity);
            context.SaveChanges();
            return entry.Entity;
        }

        public IEnumerable<CellEntity> Add(
            IEnumerable<CellEntity> newCellEntities)
        {
            {
                var addedEntities = new List<CellEntity>();
                newCellEntities.ToList().ForEach(cellEntity =>
                {
                    using var context = new CellContext();
                    var entry = context.Add(cellEntity);
                    addedEntities.Add(entry.Entity);
                    context.SaveChanges();
                });
                return addedEntities;
            }
        }

        public async Task RemoveAllAsync()
        {
            await using var context = new CellContext();
            context.RemoveRange(context.Cells);
            await context.SaveChangesAsync();
        }

        public void RemoveAll()
        {
            using var context = new CellContext();
            context.RemoveRange(context.Cells);
            context.SaveChanges();
        }

        public void Update(CellEntity changedCellEntity)
        {
            using var context = new CellContext();
            context.Update(changedCellEntity);
            context.SaveChanges();
        }

        public void Remove(CellEntity cellEntityToRemove)
        {
            using var context = new CellContext();
            context.Remove(cellEntityToRemove);
            context.SaveChanges();
        }
    }
}