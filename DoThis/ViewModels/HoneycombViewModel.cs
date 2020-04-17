using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using Beeffective.Commands;
using Beeffective.Extensions;
using Beeffective.Models;
using Beeffective.ViewModels.CellMenu;

namespace Beeffective.ViewModels
{
    public class HoneycombViewModel : ViewModel
    {
        private double width;
        private double height;
        private double zoomFactor;
        private CellViewModel selectedCell;
        private Visibility titleVisibility;
        private readonly Timer timer;
        private bool isTimerEnabled;
        private DateTime? lastUpdate;
        private ObservableCollection<TagViewModel> tagsList;
        private ObservableCollection<GoalViewModel> goalList;
        private double cellFontSize;

        public HoneycombViewModel()
        {
            Width = 10000;
            Height = 10000;
            ZoomFactor = 1;
            EmptyCells = new ObservableCollection<CellViewModel>();
            FullCells = new ObservableCollection<CellViewModel>();
            TagsList = new ObservableCollection<TagViewModel>();
            GoalList = new ObservableCollection<GoalViewModel>();
            TitleVisibility = Visibility.Collapsed;
            TimerCommand = new DelegateCommand(obj => IsTimerEnabled = !IsTimerEnabled);
            RemoveCellCommand = new DelegateCommand(obj => RemoveCell());
            timer = new Timer {Interval = 250};
            timer.Elapsed += OnTimerElapsed;
            CellMenu = new CellMenuViewModel();
            TitleCommand = new DelegateCommand(obj => MessageBox.Show("Title"));
        }

        public CellMenuViewModel CellMenu { get; }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (lastUpdate.HasValue)
            {
                if (SelectedCell != null)
                {
                    var delta = DateTime.Now - lastUpdate.Value;
                    SelectedCell.TimeSpent = SelectedCell.TimeSpent.Add(delta);
                    UpdateTagTimes(SelectedCell, delta);
                    UpdateGoalTimes(SelectedCell, delta);
                }
            }

            lastUpdate = DateTime.Now;
        }

        private void UpdateTagTimes(CellViewModel cellViewModel, in TimeSpan delta)
        {
            var tags = cellViewModel.SpaceSeparatedTags.Split(" ");
            foreach (string tag in tags)
            {
                var tagViewModel = TagsList.Single(t => t.Name == tag);
                tagViewModel.TimeSpent = tagViewModel.TimeSpent.Add(delta);
            }

            TagsList = new ObservableCollection<TagViewModel>(
                TagsList.OrderByDescending(t => t.TimeSpent));
        }

        private void UpdateGoalTimes(CellViewModel cellViewModel, in TimeSpan delta)
        {
            var goal = cellViewModel.Goal;

            var goalViewModel = GoalList.Single(t => t.Title == goal);
            goalViewModel.TimeSpent = goalViewModel.TimeSpent.Add(delta);

            GoalList = new ObservableCollection<GoalViewModel>(
                GoalList.OrderByDescending(t => t.TimeSpent));
        }

        public ICommand TimerCommand { get; }

        public ICommand RemoveCellCommand { get; }

        public ICommand TitleCommand { get; }

        private void RemoveCell()
        {
            if (SelectedCell == null) return;
            if (MessageBox.Show(
                    "Are you sure?", 
                    "Remove task", 
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var newCellModel = new CellModel();
                newCellModel.Urgency = SelectedCell.X;
                newCellModel.Importance = SelectedCell.Y;
                RemoveFullCell(SelectedCell);
                SelectedCell = null;
                var addedEmptyCell = AddEmptyCell(newCellModel);
                if (addedEmptyCell != null)
                {
                    EmptyCells.Add(new CellViewModel(addedEmptyCell, this));
                }
            }
        }

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

        public ObservableCollection<TagViewModel> TagsList
        {
            get => tagsList;
            set
            {
                if (Equals(tagsList, value)) return;
                tagsList = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<GoalViewModel> GoalList
        {
            get => goalList;
            set
            {
                if (Equals(goalList, value)) return;
                goalList = value;
                OnPropertyChanged();
            }
        }

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
            try
            {
                IsBusy = true;
                await Honeycomb.LoadAsync();
                EmptyCells.Clear();
                FullCells.Clear();
                TagsList.Clear();
                foreach (var cellModel in Honeycomb.Cells)
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
                UpdateTagsList();
                UpdateGoalList();
            }
            finally
            {
                IsBusy = false;
            }
        }

        public CellModel AddFullCell(CellModel newCellModel)
        {
            var addedCellModel = Honeycomb.AddCell(newCellModel);
            var newCellViewModel = new CellViewModel(addedCellModel, this);
            AddFullCell(newCellViewModel);
            return addedCellModel;
        }

        private void AddFullCell(CellViewModel cellViewModel)
        {
            cellViewModel.SelectionChanged += OnCellViewModelSelectionChanged;
            cellViewModel.PropertyChanged += OnCellViewModelPropertyChanged;
            FullCells.Add(cellViewModel);
            OnPropertyChanged(nameof(PriorityList));
        }

        public void RemoveFullCell(CellViewModel cellViewModelToRemove)
        {
            Honeycomb.RemoveCell(cellViewModelToRemove.Model);
            cellViewModelToRemove.SelectionChanged -= OnCellViewModelSelectionChanged;
            cellViewModelToRemove.PropertyChanged -= OnCellViewModelPropertyChanged;
            FullCells.Remove(cellViewModelToRemove);
            OnPropertyChanged(nameof(PriorityList));
        }

        private void OnCellViewModelSelectionChanged(object? sender, EventArgs e) => 
            SelectedCell = FullCells.FirstOrDefault(c => c.IsSelected);

        private void OnCellViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CellViewModel.SpaceSeparatedTags))
            {
                UpdateTagsList();
            }
            if (e.PropertyName == nameof(CellViewModel.Goal))
            {
                UpdateGoalList();
            }
        }

        private void UpdateTagsList()
        {
            foreach (var cellViewModel in FullCells
                .Where(c => !string.IsNullOrWhiteSpace(c.SpaceSeparatedTags)))
            {
                var tags = new List<string>();
                if (!cellViewModel.SpaceSeparatedTags.Contains(" "))
                {
                    tags.Add(cellViewModel.SpaceSeparatedTags);
                }
                else
                {
                    tags.AddRange(cellViewModel.SpaceSeparatedTags.Split(" "));
                }
                
                foreach (string tag in tags)
                {
                    var tagViewModel = TagsList.SingleOrDefault(t => t.Name == tag);
                    if (tagViewModel == null)
                    {
                        tagViewModel = new TagViewModel(tag);
                        TagsList.Add(tagViewModel);
                    }

                    tagViewModel.TimeSpent = tagViewModel.TimeSpent.Add(cellViewModel.TimeSpent);
                }
            }
            TagsList = new ObservableCollection<TagViewModel>(TagsList.OrderByDescending(t => t.TimeSpent));
        }

        private void UpdateGoalList()
        {
            foreach (var cellViewModel in FullCells
                .Where(c => !string.IsNullOrWhiteSpace(c.Goal)))
            {
                var goalViewModel = GoalList.SingleOrDefault(t => t.Title == cellViewModel.Goal);
                if (goalViewModel == null)
                {
                    goalViewModel = new GoalViewModel(cellViewModel.Goal);
                    GoalList.Add(goalViewModel);
                }

                goalViewModel.TimeSpent = goalViewModel.TimeSpent.Add(cellViewModel.TimeSpent);
            }
            GoalList = new ObservableCollection<GoalViewModel>(
                GoalList.OrderByDescending(t => t.TimeSpent));
        }

        public CellViewModel SelectedCell
        {
            get => selectedCell;
            set
            {
                if (Equals(selectedCell, value)) return;
                selectedCell = value;
                if (selectedCell != null) selectedCell.IsSelected = true;
                TitleVisibility = selectedCell != null ? Visibility.Visible : Visibility.Collapsed;
                UpdateCellFontSize();
                OnPropertyChanged();
            }
        }

        private void UpdateCellFontSize()
        {
            if (SelectedCell == null || string.IsNullOrWhiteSpace(SelectedCell.Title)) return;
            CellFontSize = SelectedCell.Title.ToCellFontSize();
        }

        public double CellFontSize
        {
            get => cellFontSize;
            set
            {
                if (Equals(cellFontSize, value)) return;
                cellFontSize = value;
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
            var occupied = Honeycomb.Cells.Any(c =>
                Math.Abs(c.Importance - newCellModel.Importance) < 0.01 && 
                Math.Abs(c.Urgency - newCellModel.Urgency) < 0.01);
            if (occupied) return null;
            var addedCellModel = Honeycomb.AddCell(newCellModel);
            var newCellViewModel = new CellViewModel(addedCellModel, this);
            EmptyCells.Add(newCellViewModel);
            return addedCellModel;
        }

        public void RemoveEmptyCell(CellViewModel cellViewModelToRemove)
        {
            Honeycomb.RemoveCell(cellViewModelToRemove.Model);
            EmptyCells.Remove(cellViewModelToRemove);
        }

        public void Release(CellViewModel viewModel)
        {
            if (IsDrag && PressedCell != viewModel)
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
