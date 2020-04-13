using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Beeffective.ViewModels;

namespace Beeffective.Views
{
    /// <summary>
    /// Interaction logic for TopmostCellView.xaml
    /// </summary>
    public partial class TopmostCellView : UserControl
    {
        private bool isMouseDrag;
        private bool isMouseDown;

        public TopmostCellView()
        {
            InitializeComponent();
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            //if (isMouseDown)
            //{
            //    isMouseDrag = true;
            //}
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //if (!isMouseDrag)
            //{
            //    if (DataContext is HoneycombViewModel viewModel)
            //    {
            //        viewModel.IsTimerEnabled = !viewModel.IsTimerEnabled;
            //    }
            //}
            //isMouseDrag = false;
            //isMouseDown = false;
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is HoneycombViewModel viewModel)
            {
                viewModel.IsTimerEnabled = !viewModel.IsTimerEnabled;
            }
        }
    }
}
