using System;
using System.ComponentModel;
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
        
        private void OnLabelMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is CellViewModel cellViewModel)
            {
                TitleLabel.Visibility = Visibility.Collapsed;
                TitleTextBox.Visibility = Visibility.Visible;
                cellViewModel.IsEditing = true;
            }
        }

        private void OnTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (DataContext is CellViewModel cellViewModel &&
                (e.Key == Key.Enter || e.Key == Key.Escape))
            {
                cellViewModel.IsEditing = false;
                TitleLabel.Visibility = Visibility.Visible;
                TitleTextBox.Visibility = Visibility.Collapsed;
            }
        }

        private void OnHexagonMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is CellViewModel cellViewModel)
            {
                cellViewModel.IsSelected = !cellViewModel.IsSelected;
            }
        }

        //private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == nameof(viewModel.IsSelected))
        //    {
        //        if (viewModel.IsSelected)
        //        {
        //            Polygon.Fill = FindResource("PrimaryHueMidBrush") as Brush;
        //            Polygon.Stroke = FindResource("PrimaryHueDarkBrush") as Brush;
        //        }
        //        else
        //        {
        //            Polygon.Fill = FindResource("PrimaryHueDarkBrush") as Brush;
        //            Polygon.Stroke = FindResource("PrimaryHueMidBrush") as Brush;
        //        }
        //    }
        //}
    }
}
