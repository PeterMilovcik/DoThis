using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Resources;
using System.Threading.Tasks;
using Beeffective.Models;

namespace Beeffective.ViewModels
{
    public class HoneycombViewModel : ViewModel
    {
        private readonly HoneycombModel model;
        private double width;
        private double height;
        private double zoomFactor;
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
                    FullCells.Add(cellViewModel);
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
            FullCells.Add(newCellViewModel);
            return addedCellModel;
        }

        public void RemoveFullCell(CellViewModel cellViewModelToRemove)
        {
            model.RemoveCell(cellViewModelToRemove.Model);
            FullCells.Remove(cellViewModelToRemove);
        }

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
    }
}
