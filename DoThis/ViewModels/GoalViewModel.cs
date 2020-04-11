using System;

namespace Beeffective.ViewModels
{
    public class GoalViewModel : ViewModel
    {
        private TimeSpan timeSpent;

        public GoalViewModel(string title)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            TimeSpent = new TimeSpan();
        }

        public string Title { get; }

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