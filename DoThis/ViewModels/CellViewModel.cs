using System.Windows;
using System.Windows.Input;
using Beeffective.Commands;
using Beeffective.Models;

namespace Beeffective.ViewModels
{
    public class CellViewModel : ViewModel
    {
        private const double DiagonalHorizontalOffset = 111;
        private const double DiagonalVerticalOffset = 62.5;
        private const double VerticalOffset = 125;
        private readonly CellModel model;
        private readonly HoneycombViewModel honeycomb;

        public CellViewModel(CellModel model, HoneycombViewModel honeycomb)
        {
            this.model = model;
            this.honeycomb = honeycomb;
            CreateNewCellCommand = new DelegateCommand(obj => CreateNewCell());
        }

        public int Id => model.Id;

        public double X
        {
            get => model.Urgency;
            set
            {
                if (Equals(model.Urgency, value)) return;
                model.Urgency = value;
                OnPropertyChanged();
            }
        }

        public double Y
        {
            get => model.Importance;
            set
            {
                if (Equals(model.Importance, value)) return;
                model.Importance = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => model.Title;
            set
            {
                if (Equals(model.Title, value)) return;
                model.Title = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsEmpty));
                OnPropertyChanged(nameof(EmptyVisibility));
                OnPropertyChanged(nameof(CellVisibility));
            }
        }

        public bool IsEmpty => string.IsNullOrWhiteSpace(Title);

        public Visibility EmptyVisibility => IsEmpty ? Visibility.Visible : Visibility.Collapsed;

        public Visibility CellVisibility => IsEmpty ? Visibility.Collapsed : Visibility.Visible;

        public ICommand CreateNewCellCommand { get; }

        private void CreateNewCell()
        {
            Title = "new";
            honeycomb.EmptyCells.Remove(this);
            honeycomb.FullCells.Add(this);
            CreateNorthCell();
            CreateNorthEastCell();
            CreateNorthWestCell();
            CreateSouthCell();
            CreateSouthEastCell();
            CreateSouthWestCell();
        }

        private void CreateNorthCell()
        {
            var cellModel = new CellModel
            {
                Urgency = X,
                Importance = Y - VerticalOffset,
            };
            honeycomb.EmptyCells.Add(new CellViewModel(cellModel, honeycomb));
        }

        private void CreateNorthEastCell()
        {
            var cellModel = new CellModel
            {
                Urgency = X + DiagonalHorizontalOffset,
                Importance = Y - DiagonalVerticalOffset,
            };
            honeycomb.EmptyCells.Add(new CellViewModel(cellModel, honeycomb));
        }

        private void CreateNorthWestCell()
        {
            var cellModel = new CellModel
            {
                Urgency = X - DiagonalHorizontalOffset,
                Importance = Y - DiagonalVerticalOffset,
            };
            honeycomb.EmptyCells.Add(new CellViewModel(cellModel, honeycomb));
        }

        private void CreateSouthCell()
        {
            var cellModel = new CellModel
            {
                Urgency = X,
                Importance = Y + VerticalOffset,
            };
            honeycomb.EmptyCells.Add(new CellViewModel(cellModel, honeycomb));
        }

        private void CreateSouthWestCell()
        {
            var cellModel = new CellModel
            {
                Urgency = X - DiagonalHorizontalOffset,
                Importance = Y + DiagonalVerticalOffset,
            };
            honeycomb.EmptyCells.Add(new CellViewModel(cellModel, honeycomb));
        }

        private void CreateSouthEastCell()
        {
            var cellModel = new CellModel
            {
                Urgency = X + DiagonalHorizontalOffset,
                Importance = Y + DiagonalVerticalOffset,
            };
            honeycomb.EmptyCells.Add(new CellViewModel(cellModel, honeycomb));
        }
    }
}
