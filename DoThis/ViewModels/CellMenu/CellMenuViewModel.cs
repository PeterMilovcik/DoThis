namespace Beeffective.ViewModels.CellMenu
{
    public class CellMenuViewModel
    {
        public CellMenuViewModel()
        {
            Title = new TitleCellMenuItemViewModel();
            Goal = new GoalCellMenuItemViewModel();
            Tags = new TagsCellMenuItemViewModel();
        }

        public TitleCellMenuItemViewModel Title { get; }
        public GoalCellMenuItemViewModel Goal { get; }
        public TagsCellMenuItemViewModel Tags { get; }
    }
}