using System;
using System.Linq;
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
        private readonly HoneycombViewModel honeycomb;
        private bool isEditing;
        private bool isSelected;

        public CellViewModel(CellModel model, HoneycombViewModel honeycomb)
        {
            Model = model;
            this.honeycomb = honeycomb;
            CreateNewCellCommand = new DelegateCommand(obj => CreateNewCell());
            IsSelected = false;
        }

        public int Id => Model.Id;

        public double X
        {
            get => Model.Urgency;
            set
            {
                if (Equals(Model.Urgency, value)) return;
                Model.Urgency = value;
                OnPropertyChanged();
            }
        }

        public double Y
        {
            get => Model.Importance;
            set
            {
                if (Equals(Model.Importance, value)) return;
                Model.Importance = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => Model.Title;
            set
            {
                if (Equals(Model.Title, value)) return;
                Model.Title = value;
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
        
        public CellModel Model { get; set; }

        public bool IsEditing
        {
            get => isEditing;
            set
            {
                if (Equals(isEditing, value)) return;
                isEditing = value;
                OnPropertyChanged();
            }
        }

        public bool IsSelected
        {
            get => isSelected;
            set
            {
                if (Equals(isSelected, value)) return;
                isSelected = value;
                if (isSelected) DeselectOtherCells();
                OnPropertyChanged();
                OnSelectionChanged();
            }
        }

        public event EventHandler SelectionChanged;

        private void OnSelectionChanged() => 
            SelectionChanged?.Invoke(this, EventArgs.Empty);

        private void DeselectOtherCells() => 
            honeycomb.FullCells.Except(new[] {this}).ToList()
                .ForEach(cell => cell.IsSelected = false);

        private void CreateNewCell()
        {
            Title = "new";
            honeycomb.RemoveEmptyCell(this);
            Model = honeycomb.AddFullCell(Model);
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
            honeycomb.AddEmptyCell(cellModel);
        }

        private void CreateNorthEastCell()
        {
            var cellModel = new CellModel
            {
                Urgency = X + DiagonalHorizontalOffset,
                Importance = Y - DiagonalVerticalOffset,
            };
            honeycomb.AddEmptyCell(cellModel);
        }

        private void CreateNorthWestCell()
        {
            var cellModel = new CellModel
            {
                Urgency = X - DiagonalHorizontalOffset,
                Importance = Y - DiagonalVerticalOffset,
            };
            honeycomb.AddEmptyCell(cellModel);
        }

        private void CreateSouthCell()
        {
            var cellModel = new CellModel
            {
                Urgency = X,
                Importance = Y + VerticalOffset,
            };
            honeycomb.AddEmptyCell(cellModel);
        }

        private void CreateSouthWestCell()
        {
            var cellModel = new CellModel
            {
                Urgency = X - DiagonalHorizontalOffset,
                Importance = Y + DiagonalVerticalOffset,
            };
            honeycomb.AddEmptyCell(cellModel);
        }

        private void CreateSouthEastCell()
        {
            var cellModel = new CellModel
            {
                Urgency = X + DiagonalHorizontalOffset,
                Importance = Y + DiagonalVerticalOffset,
            };
            honeycomb.AddEmptyCell(cellModel);
        }
    }
}
