using DoThis.Commands;
using System;
using System.Media;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace DoThis.ViewModels
{
    class TimerViewModel : ViewModel
    {
        private int remaining;
        private int fontSize;
        private int progress;
        private bool isActive;
        private TimeSpan timeSpan;
        private int totalMinutes;
        private readonly Timer timer;

        public TimerViewModel()
        {
            TimerCommand = new DelegateCommand(obj => TimerAction());
            SetTimerCommand = new DelegateCommand(SetTimer);
            TotalMinutes = 25;
            Remaining = TotalMinutes;
            timeSpan = TimeSpan.FromMinutes(TotalMinutes);
            timer = new Timer();
            timer.Elapsed += OnTimerElapsed;
            timer.Interval = 1000;
        }

        public int TotalMinutes
        {
            get => totalMinutes;
            set
            {
                if (Equals(totalMinutes, value)) return;
                totalMinutes = value;
                OnPropertyChanged();
            }
        }

        public int Remaining
        {
            get => remaining;
            set
            {
                if (Equals(remaining, value)) return;
                remaining = value;
                UpdateFontSize();
                OnPropertyChanged();
            }
        }

        public int FontSize
        {
            get => fontSize;
            set
            {
                if (Equals(fontSize, value)) return;
                fontSize = value;
                OnPropertyChanged();
            }
        }

        public int Progress
        {
            get => progress;
            set
            {
                if (Equals(progress, value)) return;
                progress = value;
                OnPropertyChanged();
            }
        }

        public bool IsActive
        {
            get => isActive;
            set
            {
                if (Equals(isActive, value)) return;
                isActive = value;
                timer.Enabled = value;
                OnPropertyChanged();
            }
        }

        public ICommand TimerCommand { get; }

        public ICommand SetTimerCommand { get; }

        private void TimerAction()
        {
            IsActive = !IsActive;
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (timeSpan - TimeSpan.FromSeconds(1) > TimeSpan.Zero)
            {
                timeSpan = timeSpan.Add((-1) * TimeSpan.FromMilliseconds(timer.Interval));
                Remaining = timeSpan.TotalMinutes > 1 
                    ? (int)timeSpan.TotalMinutes + (int)Math.Min(timeSpan.TotalSeconds, 1) 
                    : (int)timeSpan.TotalSeconds;
                Progress = (int)(timeSpan.TotalMinutes * 100 / TotalMinutes);
            }
            else
            {
                Remaining = 0;
                StopTimer();
                SystemSounds.Beep.Play();
                MessageBox.Show("Timer elapsed.", "DoThis", MessageBoxButton.OK, MessageBoxImage.Information);
                SetTimer(TotalMinutes);
                OnTimerFinished();
            }
        }

        public event EventHandler TimerFinished;

        private void OnTimerFinished() => 
            TimerFinished?.Invoke(this, EventArgs.Empty);

        private void UpdateFontSize()
        {
            switch (Remaining.ToString().Length)
            {
                case 1:
                    FontSize = 20;
                    break;
                case 2:
                    FontSize = 18;
                    break;
                default:
                    FontSize = 16;
                    break;
            }
        }

        private void StopTimer()
        {
            IsActive = false;
            timer.Stop();
        }

        private void SetTimer(object parameter)
        {
            if (int.TryParse(parameter.ToString(), out int value))
            {
                TotalMinutes = value;
                timeSpan = TimeSpan.FromMinutes(TotalMinutes);
                Remaining = (int)timeSpan.TotalMinutes;
            }
        }
    }
}
