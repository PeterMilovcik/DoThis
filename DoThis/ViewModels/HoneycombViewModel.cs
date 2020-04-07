using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using Beeffective.Models;
using Microsoft.EntityFrameworkCore.Internal;

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
            Cells = new ObservableCollection<CellViewModel>();
        }

        public ObservableCollection<CellViewModel> Cells { get; }

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
            Cells.Clear();
            foreach (var cellModel in model.Cells)
            {
                Cells.Add(new CellViewModel(cellModel, this));
            }

            if (!Cells.Any())
            {
                var cellModel = new CellModel
                {
                    Urgency = Width / 2, 
                    Importance = Height / 2, 
                    Title = "CENTER"
                };
                var cellViewModel = new CellViewModel(cellModel, this);
                Cells.Add(cellViewModel);
            }
        }
    }
}
