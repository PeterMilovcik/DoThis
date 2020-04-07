using System;
using System.Windows;
using System.Windows.Input;
using Syncfusion.Windows.Utils;

namespace Beeffective.ViewModels
{
    public abstract class AdjacentCellViewModel : ViewModel
    {
        private Visibility emptyVisibility;

        protected AdjacentCellViewModel(CellViewModel center, HoneycombViewModel honeycomb)
        {
            Center = center ?? throw new ArgumentNullException(nameof(center));
            Honeycomb = honeycomb ?? throw new ArgumentNullException(nameof(honeycomb));
            CreateNewCellCommand = new DelegateCommand(obj => CreateNewCell());
        }

        protected CellViewModel Center { get; }
        protected HoneycombViewModel Honeycomb { get; }
        protected abstract void CreateNewCell();

        public Visibility EmptyVisibility
        {
            get => emptyVisibility;
            set
            {
                if (Equals(emptyVisibility, value)) return;
                emptyVisibility = value;
                OnPropertyChanged();
            }
        }

        public ICommand CreateNewCellCommand { get; }
    }
}