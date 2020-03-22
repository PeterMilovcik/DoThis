using DoThis.Common;
using System;

namespace DoThis.Models
{
    class ItemModel : Observable
    {
        private int iterations;
        private string title;

        public int Id { get; set; }

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

        public DateTime CreatedAt { get; set; }

        public string Details { get; set; }

        public DateTime CompletedAt { get; set; }

        public int Urgency { get; set; }

        public int Importance { get; set; }

    }
}
