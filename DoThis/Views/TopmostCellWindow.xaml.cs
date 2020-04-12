using System.Windows.Input;

namespace Beeffective.Views
{
    public partial class TopmostCellWindow : IWindow
    {
        public TopmostCellWindow()
        {
            InitializeComponent();
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}
