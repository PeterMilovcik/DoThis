using System.Drawing;

namespace Beeffective.ViewModels.CellMenu
{
    public abstract class CellMenuItemViewModel : ViewModel
    {
        private int zIndex;
        private double left;
        private double top;

        protected CellMenuItemViewModel()
        {
            Left = Center.X;
            Top = Center.Y;
        }

        protected Point Center => new Point(250, 250);
        protected abstract Point Expanded { get; }

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

        public double Left
        {
            get => left;
            set
            {
                if (Equals(left, value)) return;
                left = value;
                OnPropertyChanged();
            }
        }

        public double Top
        {
            get => top;
            set
            {
                if (Equals(top, value)) return;
                top = value;
                OnPropertyChanged();
            }
        }

        public virtual void Show()
        {
            Left = Expanded.X;
            Top = Expanded.Y;
        }

        public virtual void Hide()
        {
            Left = Center.X;
            Top = Center.Y;
        }
    }
}
