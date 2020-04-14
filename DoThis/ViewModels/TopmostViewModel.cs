namespace Beeffective.ViewModels
{
    public class TopmostViewModel
    {
        public TopmostViewModel()
        {
            Center = new CellMenuItemViewModel();
            NorthEast = new CellMenuItemViewModel();
        }

        public CellMenuItemViewModel Center { get; }
        public CellMenuItemViewModel NorthEast { get; }
    }
}