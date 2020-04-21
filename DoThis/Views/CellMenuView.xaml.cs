using System.Windows.Input;
using Beeffective.ViewModels;

namespace Beeffective.Views
{
    public partial class CellMenuView
    {
        public CellMenuView()
        {
            InitializeComponent();
        }

        private void OnTitleMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is HoneycombViewModel viewModel)
            {
                ToggleCellMenu(viewModel);
                viewModel.CellMenu.Title.ZIndex = 1;
            }
        }

        private void OnTagsMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is HoneycombViewModel viewModel)
            {
                ToggleCellMenu(viewModel);
                viewModel.CellMenu.Tags.ZIndex = 1;
            }
        }

        private void OnRemoveMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is HoneycombViewModel viewModel)
            {
                ToggleCellMenu(viewModel);
                viewModel.CellMenu.Remove.ZIndex = 1;
                viewModel.RemoveCellCommand.Execute(null);
            }
        }

        private void OnPomodoroMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is HoneycombViewModel viewModel)
            {
                ToggleCellMenu(viewModel);
                viewModel.CellMenu.Pomodoro.ZIndex = 1;
            }
        }

        private void OnTimeTrackerMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is HoneycombViewModel viewModel)
            {
                ToggleCellMenu(viewModel);
                viewModel.CellMenu.TimeTracker.ZIndex = 1;
                viewModel.IsTimerEnabled = !viewModel.CellMenu.IsMenuShown;
            }
        }

        private void OnGoalMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is HoneycombViewModel viewModel)
            {
                ToggleCellMenu(viewModel);
                viewModel.CellMenu.Goal.ZIndex = 1;
            }
        }

        private void OnFinishMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is HoneycombViewModel viewModel)
            {
                ToggleCellMenu(viewModel);
                viewModel.CellMenu.Finish.ZIndex = 1;
            }
        }

        private static void ToggleCellMenu(HoneycombViewModel viewModel)
        {
            viewModel.CellMenu.Title.ZIndex = 0;
            viewModel.CellMenu.Finish.ZIndex = 0;
            viewModel.CellMenu.Goal.ZIndex = 0;
            viewModel.CellMenu.Pomodoro.ZIndex = 0;
            viewModel.CellMenu.TimeTracker.ZIndex = 0;
            viewModel.CellMenu.Tags.ZIndex = 0;
            viewModel.CellMenu.Remove.ZIndex = 0;

            viewModel.CellMenu.IsMenuShown = !viewModel.CellMenu.IsMenuShown;
            if (viewModel.CellMenu.IsMenuShown)
            {
                viewModel.CellMenu.Tags.Show();
                viewModel.CellMenu.Remove.Show();
                viewModel.CellMenu.Finish.Show();
                viewModel.CellMenu.Goal.Show();
                viewModel.CellMenu.Pomodoro.Show();
                viewModel.CellMenu.TimeTracker.Show();
            }
            else
            {
                viewModel.CellMenu.Tags.Hide();
                viewModel.CellMenu.Remove.Hide();
                viewModel.CellMenu.Finish.Hide();
                viewModel.CellMenu.Goal.Hide();
                viewModel.CellMenu.Pomodoro.Hide();
                viewModel.CellMenu.TimeTracker.Hide();
            }
        }
    }
}
