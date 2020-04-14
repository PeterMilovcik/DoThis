using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Beeffective.Controls
{
    /// <summary>
    /// Interaction logic for Hexagon.xaml
    /// </summary>
    public partial class Hexagon : UserControl
    {
        public Hexagon()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty FillProperty = DependencyProperty.Register(
            "Fill", typeof(Brush), typeof(Hexagon), new PropertyMetadata(default(Brush)));

        public Brush Fill
        {
            get => GetValue(FillProperty) as Brush;
            set => SetValue(FillProperty, value);
        }

        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(
            "Stroke", typeof(Brush), typeof(Hexagon), new PropertyMetadata(default(Brush)));

        public Brush Stroke
        {
            get => GetValue(StrokeProperty) as Brush;
            set => SetValue(StrokeProperty, value);
        }

        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(
            "StrokeThickness", typeof(double), typeof(Hexagon), new PropertyMetadata(default(double)));

        public double StrokeThickness
        {
            get => (double) GetValue(StrokeThicknessProperty);
            set => SetValue(StrokeThicknessProperty, value);
        }
    }
}
