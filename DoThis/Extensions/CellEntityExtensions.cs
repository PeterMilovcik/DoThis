﻿using System;
using Beeffective.Data.Entities;
using Beeffective.Models;

namespace Beeffective.Extensions
{
    static class CellEntityExtensions
    {
        public static CellModel ToCellModel(this CellEntity cellEntity)
        {
            if (cellEntity == null)
                throw new ArgumentNullException(nameof(cellEntity));

            return new CellModel
            {
                Id = cellEntity.Id,
                Title = cellEntity.Title,
                Urgency = cellEntity.Urgency,
                Importance = cellEntity.Importance,
                Goal = cellEntity.Goal,
                Tags = cellEntity.Tags,
                TimeSpent = cellEntity.TimeSpent
            };
        }
    }
}
