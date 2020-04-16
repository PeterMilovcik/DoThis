using System.Windows;
using System.Windows.Controls;

namespace Beeffective.Views
{
    public partial class PomodoroCellMenuItemView : UserControl
    {
        public PomodoroCellMenuItemView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", 
            typeof(string), 
            typeof(PomodoroCellMenuItemView), 
            new PropertyMetadata(default(string)));

        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
    }
}
