using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using Beeffective.Commands;
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
        private Timer timer;
        private bool isTimerEnabled;
        private DateTime? lastUpdate;

        public HoneycombViewModel(HoneycombModel model)
        {
            this.model = model ?? throw new ArgumentNullException(nameof(model));
            Width = 10000;
            Height = 10000;
            ZoomFactor = 1;
            EmptyCells = new ObservableCollection<CellViewModel>();
            FullCells = new ObservableCollection<CellViewModel>();
            TitleVisibility = Visibility.Collapsed;
            TimerCommand = new DelegateCommand(obj => IsTimerEnabled = !IsTimerEnabled);
            timer = new Timer {Interval = 250};
            timer.Elapsed += OnTimerElapsed;
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (lastUpdate.HasValue)
            {
                if (SelectedCell != null)
                {
                    var delta = DateTime.Now - lastUpdate.Value;
                    SelectedCell.TimeSpent = SelectedCell.TimeSpent.Add(delta);
                }
            }

            lastUpdate = DateTime.Now;
        }

        public ICommand TimerCommand { get; }

        public bool IsTimerEnabled
        {
            get => isTimerEnabled;
            set
            {
                if (Equals(isTimerEnabled, value)) return;
                isTimerEnabled = value;
                if (!isTimerEnabled) lastUpdate = null;
                timer.Enabled = isTimerEnabled;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<CellViewModel> EmptyCells { get; }

        public ObservableCollection<CellViewModel> FullCells { get; }

        public IOrderedEnumerable<CellViewModel> PriorityList => 
            FullCells.OrderBy(c => c.Priority);

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
            OnPropertyChanged(nameof(PriorityList));
        }

        public void RemoveFullCell(CellViewModel cellViewModelToRemove)
        {
            model.RemoveCell(cellViewModelToRemove.Model);
            cellViewModelToRemove.SelectionChanged -= OnCellViewModelSelectionChanged;
            FullCells.Remove(cellViewModelToRemove);
            OnPropertyChanged(nameof(PriorityList));
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
                if (selectedCell != null) selectedCell.IsSelected = true;
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
                OnPropertyChanged(nameof(PriorityList));
            }

            PressedCell = null;
            IsDrag = false;
        }
    }
}
