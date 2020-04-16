using System.Windows;
using System.Windows.Controls;

namespace Beeffective.Views
{
    public partial class RemoveCellMenuItemView : UserControl
    {
        public RemoveCellMenuItemView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", 
            typeof(string), 
            typeof(RemoveCellMenuItemView), 
            new PropertyMetadata(default(string)));

        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
    }
}
