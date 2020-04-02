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

namespace Beeffective.Views
{
    /// <summary>
    /// Interaction logic for HoneycombView.xaml
    /// </summary>
    public partial class HoneycombView : UserControl
    {
        private double zoomMax = 5;
        private double zoomMin = 0.5;
        private double zoomSpeed = 0.001;
        private double zoom = 1;

        public HoneycombView()
        {
            InitializeComponent();
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            zoom += zoomSpeed * e.Delta; // Ajust zooming speed (e.Delta = Mouse spin value )
            if (zoom < zoomMin) { zoom = zoomMin; } // Limit Min Scale
            if (zoom > zoomMax) { zoom = zoomMax; } // Limit Max Scale

            Point mousePos = e.GetPosition(Canvas);

            if (zoom > 1)
            {
                Canvas.RenderTransform = new ScaleTransform(zoom, zoom, mousePos.X, mousePos.Y); // transform Canvas size from mouse position
            }
            else
            {
                Canvas.RenderTransform = new ScaleTransform(zoom, zoom); // transform Canvas size
            }
        }
    }
}
