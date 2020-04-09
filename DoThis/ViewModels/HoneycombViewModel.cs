using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using Beeffective.Models;

namespace Beeffective.ViewModels
{
    public class HoneycombViewModel : ViewModel
    {
        private readonly HoneycombModel model;
        private double width;
        private double height;
        private double zoomFactor;
        private CellViewModel selectedCell;
        private Visibility titleVisibility;
        private const double CellHeight = 125;
        private const double CellWidth = 110;
        
        public HoneycombViewModel(HoneycombModel model)
        {
            this.model = model ?? throw new ArgumentNullException(nameof(model));
            Width = 10000;
            Height = 10000;
            ZoomFactor = 1;
            EmptyCells = new ObservableCollection<CellViewModel>();
            FullCells = new ObservableCollection<CellViewModel>();
            TitleVisibility = Visibility.Collapsed;
        }

        public ObservableCollection<CellViewModel> EmptyCells { get; }

        public ObservableCollection<CellViewModel> FullCells { get; }


        public double ZoomFactor
        {
            get => zoomFactor;
            set
            {
                if (Equals(zoomFactor, value)) return;
                zoomFactor = value;
                OnPropertyChanged();
            }
        }

        public double Width
        {
            get => width;
            set
            {
                if (Equals(width, value)) return;
                width = value;
                OnPropertyChanged();
            }
        }

        public double Height
        {
            get => height;
            set
            {
                if (Equals(height, value)) return;
                height = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAsync()
        {
            await model.LoadAsync();
            EmptyCells.Clear();
            FullCells.Clear();
            foreach (var cellModel in model.Cells)
            {
                var cellViewModel = new CellViewModel(cellModel, this);
                if (cellViewModel.IsEmpty)
                {
                    EmptyCells.Add(cellViewModel);
                }
                else
                {
                    AddFullCell(cellViewModel);
                }
            }

            if (!FullCells.Any())
            {
                var cellModel = new CellModel
                {
                    Urgency = Width / 2, 
                    Importance = Height / 2, 
                    Title = string.Empty,
                };
                var cellViewModel = new CellViewModel(cellModel, this);
                EmptyCells.Add(cellViewModel);
            }
        }

        public CellModel AddFullCell(CellModel newCellModel)
        {
            var addedCellModel = model.AddCell(newCellModel);
            var newCellViewModel = new CellViewModel(addedCellModel, this);
            AddFullCell(newCellViewModel);
            return addedCellModel;
        }

        private void AddFullCell(CellViewModel cellViewModel)
        {
            cellViewModel.SelectionChanged += OnCellViewModelSelectionChanged;
            FullCells.Add(cellViewModel);
        }

        public void RemoveFullCell(CellViewModel cellViewModelToRemove)
        {
            model.RemoveCell(cellViewModelToRemove.Model);
            cellViewModelToRemove.SelectionChanged -= OnCellViewModelSelectionChanged;
            FullCells.Remove(cellViewModelToRemove);
        }

        private void OnCellViewModelSelectionChanged(object? sender, EventArgs e) => 
            SelectedCell = FullCells.FirstOrDefault(c => c.IsSelected);

        public CellViewModel SelectedCell
        {
            get => selectedCell;
            set
            {
                if (Equals(selectedCell, value)) return;
                selectedCell = value;
                TitleVisibility = selectedCell != null ? Visibility.Visible : Visibility.Collapsed;
                OnPropertyChanged();
            }
        }

        public Visibility TitleVisibility
        {
            get => titleVisibility;
            set
            {
                if (Equals(titleVisibility, value)) return;
                titleVisibility = value;
                OnPropertyChanged();
            }
        }

        public CellViewModel PressedCell { get; set; }
        public bool IsDrag { get; set; }

        public CellModel AddEmptyCell(CellModel newCellModel)
        {
            var addedCellModel = model.AddCell(newCellModel);
            var newCellViewModel = new CellViewModel(addedCellModel, this);
            EmptyCells.Add(newCellViewModel);
            return addedCellModel;
        }

        public void RemoveEmptyCell(CellViewModel cellViewModelToRemove)
        {
            model.RemoveCell(cellViewModelToRemove.Model);
            EmptyCells.Remove(cellViewModelToRemove);
        }

        public void Release(CellViewModel viewModel)
        {
            if (IsDrag && PressedCell != viewModel && !viewModel.IsEmpty)
            {
                var startX = PressedCell.X;
                var startY = PressedCell.Y;
                var stopX = viewModel.X;
                var stopY = viewModel.Y;
                viewModel.X = startX;
                viewModel.Y = startY;
                PressedCell.X = stopX;
                PressedCell.Y = stopY;
            }

            PressedCell = null;
            IsDrag = false;
        }
    }
}
