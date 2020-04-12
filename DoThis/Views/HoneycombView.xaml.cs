
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Beeffective.Data;
using Beeffective.Extensions;
using Beeffective.Models;
using Beeffective.ViewModels;

namespace Beeffective.Views
{
    /// <summary>
    /// Interaction logic for HoneycombView.xaml
    /// </summary>
    public partial class HoneycombView : UserControl
    {
        private readonly HoneycombViewModel viewModel;

        public HoneycombView()
        {
            var repository = new CellRepository();
            var model = new HoneycombModel(repository);
            var topMostWindow = new TopmostCellWindow();
            viewModel = new HoneycombViewModel(model, topMostWindow);
            DataContext = viewModel;
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        { 
            base.OnInitialized(e);
            double offsetX = Background.Width / 2;
            double offsetY = Background.Height / 2;
            Scroll.ScrollToHorizontalOffset(offsetX - SystemParameters.WorkArea.Width / 2);
            Scroll.ScrollToVerticalOffset(offsetY -SystemParameters.WorkArea.Height / 2);
            viewModel.LoadAsync().FireAndForgetSafeAsync();
        }

        private void OnCanvasMouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void OnCanvasMouseMove(object sender, MouseEventArgs e)
        {
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            var position = e.GetPosition(Background);

            double offsetX = Background.ActualWidth / 2 - position.X;
            double offsetY = Background.ActualHeight / 2 - position.Y;

            Scroll.ScrollToHorizontalOffset(offsetX);
            Scroll.ScrollToVerticalOffset(offsetY);
        }
    }
}
