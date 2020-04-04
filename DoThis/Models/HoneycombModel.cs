using System.Collections.Generic;

namespace Beeffective.Models
{
    public class HoneycombModel
    {
        private readonly List<CellModel> cells;

        public HoneycombModel()
        {
            cells = new List<CellModel>();
        }

        public IReadOnlyList<CellModel> Cells => cells;

        public void AddCell(CellModel newCellModel) => cells.Add(newCellModel);

        public void RemoveCell(CellModel cellToRemove) => cells.Remove(cellToRemove);
    }
}
