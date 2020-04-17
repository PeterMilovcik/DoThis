using System;
using System.Windows;
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
                viewModel.IsMenuShown = !viewModel.IsMenuShown;
            }
        }

        private void OnTagsMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is HoneycombViewModel viewModel)
            {
                viewModel.IsMenuShown = !viewModel.IsMenuShown;
            }
        }

        private void OnRemoveMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is HoneycombViewModel viewModel)
            {
                viewModel.RemoveCellCommand.Execute(null);
                //viewModel.IsMenuShown = !viewModel.IsMenuShown;
            }
        }

        private void OnPomodoroMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is HoneycombViewModel viewModel)
            {
                viewModel.IsMenuShown = !viewModel.IsMenuShown;
            }
        }

        private void OnTimeTrackerMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is HoneycombViewModel viewModel)
            {
                viewModel.IsMenuShown = !viewModel.IsMenuShown;
            }
        }

        private void OnGoalMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is HoneycombViewModel viewModel)
            {
                viewModel.IsMenuShown = !viewModel.IsMenuShown;
            }
        }

        private void OnFinishMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is HoneycombViewModel viewModel)
            {
                viewModel.IsMenuShown = !viewModel.IsMenuShown;
            }
        }
    }
}
