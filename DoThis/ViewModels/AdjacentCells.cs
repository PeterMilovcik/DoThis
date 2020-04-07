namespace Beeffective.ViewModels
{
    public class AdjacentCells
    {
        public AdjacentCells(CellViewModel center, HoneycombViewModel honeycomb)
        {
            SouthWest = new SouthWestAdjacentCellViewModel(center, honeycomb);
            NorthWest = new NorthWestAdjacentCellViewModel(center, honeycomb);
            SouthEast = new SouthEastAdjacentCellViewModel(center, honeycomb);
            NorthEast = new NorthEastAdjacentCellViewModel(center, honeycomb);
            South = new SouthAdjacentCellViewModel(center, honeycomb);
            North = new NorthAdjacentCellViewModel(center, honeycomb);
        }

        public AdjacentCellViewModel SouthWest { get; }
        public AdjacentCellViewModel NorthWest { get; }
        public AdjacentCellViewModel SouthEast { get; }
        public AdjacentCellViewModel NorthEast { get; }
        public AdjacentCellViewModel South { get; }
        public AdjacentCellViewModel North { get; }
    }
}