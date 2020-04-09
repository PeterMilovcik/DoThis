using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Beeffective.ViewModels;

namespace Beeffective.Views
{
    /// <summary>
    /// Interaction logic for CellView.xaml
    /// </summary>
    public partial class CellView : UserControl
    {
        public CellView()
        {
            InitializeComponent();
        }
        
        private void OnHexagonMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("OnHexagonMouseLeftButtonDown");
            if (DataContext is CellViewModel viewModel)
            {
                Debug.WriteLine(string.IsNullOrEmpty(viewModel.Title) ? "empty" : viewModel.Title);
                viewModel.Honeycomb.PressedCell = viewModel;
            }
        }

        private void OnHexagonMouseMove(object sender, MouseEventArgs e)
        {
            if (DataContext is CellViewModel viewModel)
            {
                Debug.WriteLine("OnHexagonMouseMove");
                if (viewModel.Honeycomb.PressedCell != null)
                {
                    Debug.WriteLine(string.IsNullOrEmpty(viewModel.Title) ? "empty" : viewModel.Title);
                    viewModel.Honeycomb.IsDrag = true;
                }
            }
        }

        private void OnHexagonMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("OnHexagonMouseLeftButtonUp");
            if (DataContext is CellViewModel viewModel)
            {
                Debug.WriteLine(string.IsNullOrEmpty(viewModel.Title) ? "empty" : viewModel.Title);
                if (!viewModel.Honeycomb.IsDrag)
                {
                    viewModel.IsSelected = !viewModel.IsSelected;
                }
                viewModel.Honeycomb.Release(viewModel);
            }
        }
    }
}
