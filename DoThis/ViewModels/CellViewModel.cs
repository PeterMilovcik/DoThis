using Beeffective.Models;

namespace Beeffective.ViewModels
{
    public class CellViewModel : ViewModel
    {
        private readonly CellModel model;
        
        public CellViewModel(CellModel model, HoneycombViewModel honeycomb)
        {
            this.model = model;
            AdjacentCells = new AdjacentCells(this, honeycomb);
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
            }
        }

        public AdjacentCells AdjacentCells { get; }
    }
}
