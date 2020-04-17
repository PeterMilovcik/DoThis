using System.Windows.Input;
using Beeffective.ViewModels;

namespace Beeffective.Views
{
    public partial class CellMenuWindow : ICellMenuWindow
    {
        public CellMenuWindow(HoneycombViewModel viewModel)
        {
            DataContext = viewModel;
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
