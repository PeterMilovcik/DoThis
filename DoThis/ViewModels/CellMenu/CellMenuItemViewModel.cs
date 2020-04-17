namespace Beeffective.ViewModels.CellMenu
{
    public class CellMenuItemViewModel : ViewModel
    {
        private int zIndex;

        public int ZIndex
        {
            get => zIndex;
            set
            {
                if (Equals(zIndex, value)) return;
                zIndex = value;
                OnPropertyChanged();
            }
        }
    }
}
