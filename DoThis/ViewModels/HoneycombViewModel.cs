namespace Beeffective.ViewModels
{
    class HoneycombViewModel : ViewModel
    {
        private double width;
        private double height;

        public HoneycombViewModel()
        {
            Width = 1000;
            Height = 1000;
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
    }
}
