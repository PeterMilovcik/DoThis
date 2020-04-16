using System.Windows.Input;

namespace Beeffective.Views
{
    public partial class TopmostCellWindow : IWindow
    {
        public TopmostCellWindow()
        {
            InitializeComponent();
        }
        
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
