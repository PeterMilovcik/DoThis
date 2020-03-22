using DoThis.Common;

namespace DoThis.Models
{
    class ItemModel : Observable
    {
        private int iterations;
        private string title;

        public string Title
        {
            get => title;
            set
            {
                if (Equals(title, value)) return;
                title = value;
                OnPropertyChanged();
            }
        }

        public int Iterations
        {
            get => iterations;
            set
            {
                if (Equals(iterations, value)) return;
                iterations = value;
                OnPropertyChanged();
            }
        }
    }
}
