using System.Windows;
using System.Windows.Controls;

namespace Beeffective.Views
{
    public partial class TagsCellMenuItemView : UserControl
    {
        public TagsCellMenuItemView()
        {
             InitializeComponent();
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", 
            typeof(string), 
            typeof(TagsCellMenuItemView), 
            new PropertyMetadata(default(string)));

        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
    }
}
