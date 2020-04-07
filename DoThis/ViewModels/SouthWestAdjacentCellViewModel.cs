using System.Windows;
using Beeffective.Models;

namespace Beeffective.ViewModels
{
    class SouthWestAdjacentCellViewModel : AdjacentCellViewModel
    {
        public SouthWestAdjacentCellViewModel(CellViewModel center, HoneycombViewModel honeycomb) : base(center, honeycomb)
        {
        }

        protected override void CreateNewCell()
        {
            EmptyVisibility = Visibility.Collapsed;
            var cellModel = new CellModel
            {
                Title = "new",
                Urgency = Center.X - 109,
                Importance = Center.Y + 62
            };
            var newCellViewModel = new CellViewModel(cellModel, Honeycomb);
            newCellViewModel.AdjacentCells.NorthEast.EmptyVisibility = Visibility.Collapsed;
            Honeycomb.Cells.Add(newCellViewModel);
        }
    }
}