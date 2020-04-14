using System.Windows;
using System.Windows.Controls;

namespace Beeffective.Views
{
    /// <summary>
    /// Interaction logic for TitleCellMenuItemView.xaml
    /// </summary>
    public partial class TitleCellMenuItemView : UserControl
    {
        public TitleCellMenuItemView()
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
