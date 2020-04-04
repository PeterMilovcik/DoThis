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
        private readonly CellContext context;

        public CellRepository()
        {
            context = new CellContext();
        }

        public Task<List<CellEntity>> LoadAsync() => 
            context.Cells.ToListAsync();

        public async Task SaveAsync() => 
            await context.SaveChangesAsync(CancellationToken.None);

        public async Task<CellEntity> AddAsync(CellEntity entity)
        {
            var entry = await context.AddAsync(entity, CancellationToken.None);
            await context.SaveChangesAsync(CancellationToken.None);
            return entry.Entity;
        }

        public async Task<CellEntity> UpdateAsync(CellEntity entity)
        {
            var entry = context.Update(entity);
            await context.SaveChangesAsync(CancellationToken.None);
            return entry.Entity;
        }

        public async Task RemoveAsync(CellEntity entity)
        {
            context.Remove(entity);
            await context.SaveChangesAsync(CancellationToken.None);
        }

        public void Add(params CellEntity[] newCellEntities)
        {
            newCellEntities.ToList().ForEach(cellEntity => context.Add(cellEntity));
            context.SaveChanges();
        }

        public async Task RemoveAllAsync()
        {
            context.RemoveRange(context.Cells);
            await context.SaveChangesAsync();
        }

        public void RemoveAll()
        {
            context.RemoveRange(context.Cells);
            context.SaveChanges();
        }

        public void Update(CellEntity changedCellEntity)
        {
            context.Update(changedCellEntity);
            context.SaveChanges();
        }

        public void Remove(CellEntity cellEntityToRemove)
        {
            context.Remove(cellEntityToRemove);
            context.SaveChanges();
        }
    }
}