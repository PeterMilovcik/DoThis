using System.Windows;
using System.Windows.Controls;

namespace Beeffective.Views
{
    public partial class GoalCellMenuItemView : UserControl
    {
        public GoalCellMenuItemView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", 
            typeof(string), 
            typeof(GoalCellMenuItemView), 
            new PropertyMetadata(default(string)));

        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
    }
}
