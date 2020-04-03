namespace Beeffective.ViewModels
{
    class CellViewModel : ViewModel
    {
        private string text;
        private double x;
        private double y;

        public double X
        {
            get => x;
            set
            {
                if (Equals(x, value)) return;
                x = value;
                OnPropertyChanged();
            }
        }

        public double Y
        {
            get => y;
            set
            {
                if (Equals(y, value)) return;
                y = value;
                OnPropertyChanged();
            }
        }


        public string Text
        {
            get => text;
            set
            {
                if (Equals(text, value)) return;
                text = value;
                OnPropertyChanged();
            }
        }
    }
}
