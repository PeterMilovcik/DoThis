using System.Reflection.Emit;

namespace Beeffective.ViewModels.CellMenu
{
    public class CellMenuViewModel : ViewModel
    {
        private bool isMenuShown;

        public CellMenuViewModel()
        {
            Title = new TitleCellMenuItemViewModel();
            Goal = new GoalCellMenuItemViewModel();
            Tags = new TagsCellMenuItemViewModel();
            Remove = new RemoveCellMenuItemViewModel();
            Finish = new FinishCellMenuItemViewModel();
            TimeTracker = new TimeTrackerCellMenuItemViewModel();
            Pomodoro = new PomodoroCellMenuItemViewModel();
        }

        public bool IsMenuShown
        {
            get => isMenuShown;
            set
            {
                if (Equals(isMenuShown, value)) return;
                isMenuShown = value;
                OnPropertyChanged();
            }
        }

        public TitleCellMenuItemViewModel Title { get; }
        public GoalCellMenuItemViewModel Goal { get; }
        public TagsCellMenuItemViewModel Tags { get; }
        public RemoveCellMenuItemViewModel Remove { get; }
        public FinishCellMenuItemViewModel Finish { get; }
        public TimeTrackerCellMenuItemViewModel TimeTracker { get; }
        public PomodoroCellMenuItemViewModel Pomodoro { get; }
    }
}