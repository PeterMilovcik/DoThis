using System;
using System.Collections.Generic;
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
        private const double CellHeight = 125;
        private const double CellWidth = 110;

        public HoneycombViewModel(HoneycombModel model)
        {
            this.model = model ?? throw new ArgumentNullException(nameof(model));
            Width = 5000;
            Height = 5000;
            ZoomFactor = 1;
            Cells = new ObservableCollection<CellViewModel>();
            //Cells = new ObservableCollection<CellViewModel>
            //{
            //    new CellViewModel {X = 2500, Y = 2500 + 0 * CellHeight, Text = "A"},
            //    new CellViewModel {X = 2500, Y = 2500 + 1 * CellHeight, Text = "B"},
            //    new CellViewModel {X = 2500, Y = 2500 + 2 * CellHeight, Text = "C"},
            //    new CellViewModel {X = 2500, Y = 2500 + 3 * CellHeight, Text = "D"},
            //    new CellViewModel {X = 2500, Y = 2500 + 4 * CellHeight, Text = "E"},

            //    new CellViewModel {X = 2500 + 1 * CellWidth, Y = 2437.5 + 0 * CellHeight, Text = "F"},
            //    new CellViewModel {X = 2500 + 1 * CellWidth, Y = 2437.5 + 1 * CellHeight, Text = "G"},
            //    new CellViewModel {X = 2500 + 1 * CellWidth, Y = 2437.5 + 2 * CellHeight, Text = "H"},
            //    new CellViewModel {X = 2500 + 1 * CellWidth, Y = 2437.5 + 3 * CellHeight, Text = "I"},
            //    new CellViewModel {X = 2500 + 1 * CellWidth, Y = 2437.5 + 4 * CellHeight, Text = "K"},

            //    new CellViewModel {X = 2500 - 1 * CellWidth, Y = 2437.5 + 0 * CellHeight, Text = "L"},
            //    new CellViewModel {X = 2500 - 1 * CellWidth, Y = 2437.5 + 1 * CellHeight, Text = "M"},
            //    new CellViewModel {X = 2500 - 1 * CellWidth, Y = 2437.5 + 2 * CellHeight, Text = "N"},
            //    new CellViewModel {X = 2500 - 1 * CellWidth, Y = 2437.5 + 3 * CellHeight, Text = "O"},
            //    new CellViewModel {X = 2500 - 1 * CellWidth, Y = 2437.5 + 4 * CellHeight, Text = "P"},
            //};
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
                Cells.Add(new CellViewModel(cellModel));
            }
        }

        public void Click(Point point)
        {
            const double height = 125;
            const double width = 105;


            var xList = new List<double>();
            for (int i = 0; i < Width / width; i++)
            {
                xList.Add(i * width);
            }
            var yList = new List<double>();
            for (int i = 0; i < Height / height; i++)
            {
                yList.Add(i * height);
            }

            double min = 0;
            double max = Width;
            double urgency = 0;
            for (int i = 0; i < xList.Count; i++)
            {
                if (xList[i] > point.X) min = xList[i];
                if (xList[i] < point.X) max = xList[i];
            }

            urgency = Math.Abs(point.X - min) < Math.Abs(point.X - max) ? min : max;

            min = 0;
            max = Height;
            double importance = 0;
            for (int i = 0; i < yList.Count; i++)
            {
                if (yList[i] > point.Y) min = yList[i];
                if (yList[i] < point.Y) max = yList[i];
            }

            importance = Math.Abs(point.Y - min) < Math.Abs(point.Y - max) ? min : max;

            var cellModel = new CellModel
            {
                Title = "test", 
                Urgency = urgency, 
                Importance = importance
            };
            Cells.Add(new CellViewModel(cellModel));
        }
    }
}
