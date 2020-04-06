using System;
using Beeffective.Data.Entities;
using Beeffective.Models;

namespace Beeffective.Extensions
{
    static class CellModelExtensions
    {
        public static CellEntity ToCellEntity(this CellModel cellModel)
        {
            if (cellModel == null)
                throw new ArgumentNullException(nameof(cellModel));

            return new CellEntity
            {
                Id = cellModel.Id,
                Title = cellModel.Title,
                Urgency = cellModel.Urgency,
                Importance = cellModel.Importance
            };
        }
    }
}
