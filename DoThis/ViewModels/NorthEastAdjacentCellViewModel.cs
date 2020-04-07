namespace Beeffective.ViewModels
{
    public class NorthEastAdjacentCellViewModel : AdjacentCellViewModel
    {
        public NorthEastAdjacentCellViewModel(CellViewModel center, HoneycombViewModel honeycomb) : base(center, honeycomb)
        {
        }

        protected override void CreateNewCell()
        {
        }
    }
}