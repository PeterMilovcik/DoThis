namespace Beeffective.ViewModels
{
    public class SouthAdjacentCellViewModel : AdjacentCellViewModel
    {
        public SouthAdjacentCellViewModel(CellViewModel center, HoneycombViewModel honeycomb) 
            : base(center, honeycomb)
        {
        }

        protected override void CreateNewCell()
        {
            
        }
    }
}