using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Beeffective.Data;
using Beeffective.Data.Entities;
using Beeffective.Extensions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Beeffective.Models
{
    public class HoneycombModel
    {
        private readonly IRepository<CellEntity> repository;
        private readonly List<CellModel> cells;

        public HoneycombModel(IRepository<CellEntity> repository)
        {
            this.repository = repository;
            cells = new List<CellModel>();
        }

        public IReadOnlyList<CellModel> Cells => cells;

        public CellModel AddCell(CellModel newCellModel)
        {
            var addedCellEntity = repository.Add(newCellModel.ToCellEntity());
            if (addedCellEntity == null) return null;
            var addedCellModel = addedCellEntity.ToCellModel();
            Subscribe(addedCellModel);
            cells.Add(addedCellModel);
            return addedCellModel;
        }

        public void RemoveCell(CellModel cellToRemove)
        {
            if (!cells.Contains(cellToRemove)) return;
            Unsubscribe(cellToRemove);
            cells.Remove(cellToRemove);
            repository.Remove(cellToRemove.ToCellEntity());
        }

        public async Task<CellModel> AddCellAsync(CellModel newCellModel)
        {
            var newCellEntity = new CellEntity {Title = newCellModel.Title};
            var addedCellEntity = await repository.AddAsync(newCellEntity);
            var addedCellModel = addedCellEntity.ToCellModel();
            Subscribe(addedCellModel);
            cells.Add(addedCellModel);
            return addedCellModel;
        }

        public void Load()
        {
            var cellEntities = repository
                .LoadAsync().GetAwaiter().GetResult();
            cells.Clear();
            cells.AddRange(cellEntities.Select(
                cellEntity =>
                {
                    var cellModel = cellEntity.ToCellModel();
                    Subscribe(cellModel);
                    return cellModel;
                }));
        }

        private void Subscribe(CellModel cellModel) => 
            cellModel.PropertyChanged += OnPropertyChanged;

        private void Unsubscribe(CellModel cellModel) =>
            cellModel.PropertyChanged -= OnPropertyChanged;

        private void OnPropertyChanged(
            object sender, PropertyChangedEventArgs e)
        {
            if (sender is CellModel cellModel)
            {
                repository.UpdateAsync(cellModel.ToCellEntity())
                    .FireAndForgetSafeAsync();
            }
        }
    }
}
