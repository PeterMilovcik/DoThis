
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
            viewModel = new HoneycombViewModel(model);
            DataContext = viewModel;
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        { 
            base.OnInitialized(e);
            viewModel.LoadAsync().FireAndForgetSafeAsync();
            //Scroll.ScrollToHorizontalOffset(Scroll.ScrollableWidth / 2);
            //Scroll.ScrollToVerticalOffset(Scroll.ScrollableHeight / 2);
            //Scroll.ScrollToHorizontalOffset((Scroll.ExtentWidth - Scroll.ViewportWidth) / 2);
            //Scroll.ScrollToVerticalOffset((Scroll.ExtentHeight - Scroll.ViewportHeight) / 2);
        }

        private void OnCanvasMouseDown(object sender, MouseButtonEventArgs e)
        {
            //var position = e.GetPosition(Canvas);

            //Scroll.ScrollToHorizontalOffset(position.X - Scroll.ActualWidth / 2);
            //Scroll.ScrollToVerticalOffset(position.Y - Scroll.ActualHeight / 2);
        }

        private void OnCanvasMouseMove(object sender, MouseEventArgs e)
        {
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            var position = e.GetPosition(ItemsControl);

            //double offsetX = (position.X / ItemsControl.ActualWidth) * ItemsControl.ActualWidth;
            //double offsetY = (position.Y / ItemsControl.ActualHeight) * ItemsControl.ActualHeight;
            double offsetX = ItemsControl.ActualWidth / 2 - position.X;
            double offsetY = ItemsControl.ActualHeight / 2 - position.Y;


            Scroll.ScrollToHorizontalOffset(offsetX);
            Scroll.ScrollToVerticalOffset(offsetY);
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            viewModel.Click(e.GetPosition(ItemsControl));
        }
    }
}
