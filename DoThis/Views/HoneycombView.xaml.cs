using System;
using System.Windows;
using System.Windows.Input;
using Beeffective.Extensions;
using Beeffective.ViewModels;

namespace Beeffective.Views
{
    public partial class HoneycombView
    {
        public HoneycombView()
        {
            DataContext = App.Container.Resolve<HoneycombViewModel>();
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        { 
            base.OnInitialized(e);
            double offsetX = Background.Width / 2;
            double offsetY = Background.Height / 2;
            Scroll.ScrollToHorizontalOffset(offsetX - SystemParameters.WorkArea.Width / 2);
            Scroll.ScrollToVerticalOffset(offsetY -SystemParameters.WorkArea.Height / 2);
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
