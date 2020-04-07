namespace Beeffective.ViewModels
{
    public class NorthAdjacentCellViewModel : AdjacentCellViewModel
    {
        public NorthAdjacentCellViewModel(CellViewModel center, HoneycombViewModel honeycomb) 
            : base(center, honeycomb)
        {
        }

        protected override void CreateNewCell()
        {
            
        }
    }
}