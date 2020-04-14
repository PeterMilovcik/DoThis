using System.Windows;
using System.Windows.Controls;

namespace Beeffective.Views
{
    public partial class TimeTrackerCellMenuItemView : UserControl
    {
        public TimeTrackerCellMenuItemView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", 
            typeof(string), 
            typeof(TitleCellMenuItemView), 
            new PropertyMetadata(default(string)));

        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
    }
}
