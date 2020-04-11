using System;

namespace Beeffective.ViewModels
{
    public class TagViewModel : ViewModel
    {
        private TimeSpan timeSpent;

        public TagViewModel(string tag)
        {
            Name = tag ?? throw new ArgumentNullException(nameof(tag));
            TimeSpent = new TimeSpan();
        }

        public string Name { get; }

        public TimeSpan TimeSpent
        {
            get => timeSpent;
            set
            {
                if (Equals(timeSpent, value)) return;
                timeSpent = value;
                OnPropertyChanged();
            }
        }
    }
}
