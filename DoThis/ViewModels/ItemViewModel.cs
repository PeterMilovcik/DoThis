using System;
using System.Windows.Input;
using Beeffective.Commands;
using Beeffective.Extensions;
using Beeffective.Models;
using MaterialDesignThemes.Wpf;

namespace Beeffective.ViewModels
{
    class ItemViewModel : ViewModel
    {
        private readonly ItemModel model;
        private string title;
        private string icon;

        public ItemViewModel(ItemModel model)
        {
            this.model = model;
            UpdateIcon();
            SelectCommand = new DelegateCommand(obj => Select());
        }

        public int Id => model.Id;

        public string Title
        {
            get => model.Title;
            set
            {
                if (Equals(model.Title, value)) return;
                model.Title = value;
                SaveModel();
                OnPropertyChanged();
            }
        }

        public bool IsSelected
        {
            get => model.IsSelected;
            set
            {
                if (Equals(model.IsSelected, value)) return;
                model.IsSelected = value;
                SaveModel();
                UpdateIcon();
                model.IsSelected.Call(OnSelected);
                OnPropertyChanged();
            }
        }

        public int Iterations
        {
            get => model.Iterations;
            set
            {
                if (Equals(model.Iterations, value)) return;
                model.Iterations = value;
                SaveModel();
                OnPropertyChanged();
            }
        }

        public DateTime CreatedAt
        {
            get => model.CreatedAt;
            set
            {
                if (Equals(model.CreatedAt, value)) return;
                model.CreatedAt = value;
                SaveModel();
                OnPropertyChanged();
            }
        }

        public string Details
        {
            get => model.Details;
            set
            {
                if (Equals(model.Details, value)) return;
                model.Details = value;
                SaveModel();
                OnPropertyChanged();
            }
        }

        public DateTime CompletedAt
        {
            get => model.CompletedAt;
            set
            {
                if (Equals(model.CompletedAt, value)) return;
                model.CompletedAt = value;
                SaveModel();
                OnPropertyChanged();
            }
        }

        public int Urgency
        {
            get => model.Urgency;
            set
            {
                if (Equals(model.Urgency, value)) return;
                model.Urgency = value;
                SaveModel();
                OnPropertyChanged();
            }
        }

        public int Importance
        {
            get => model.Importance;
            set
            {
                if (Equals(model.Importance, value)) return;
                model.Importance = value;
                SaveModel();
                OnPropertyChanged();
            }
        }

        public TimeSpan AggregatedTime
        {
            get => model.AggregatedTime;
            set
            {
                if (Equals(model.AggregatedTime, value)) return;
                model.AggregatedTime = value;
                SaveModel();
                OnPropertyChanged();
            }
        }

        public string Icon
        {
            get => icon;
            set
            {
                if (Equals(icon, value)) return;
                icon = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectCommand { get; }

        private void Select() => IsSelected = !IsSelected;

        public event EventHandler Selected;

        private void OnSelected()
            => Selected?.Invoke(this, EventArgs.Empty);

        private void SaveModel()
        {
            App.Database.Update(model);
            App.Database.SaveChanges();
        }

        private void UpdateIcon() => 
            Icon = model.IsSelected ? nameof(PackIconKind.Pause) : nameof(PackIconKind.Play);
    }
}
