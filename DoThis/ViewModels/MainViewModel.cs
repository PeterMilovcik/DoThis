using DoThis.Commands;
using DoThis.Models;
using DoThis.Views;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace DoThis.ViewModels
{
    class MainViewModel : ViewModel
    {
        private const int ExpansionOffset = 40;
        private string customIcon;
        private readonly IMainView view;
        private bool isExpanded;
        private Rect workArea;
        private Visibility readOnlyTitleBarVisibility;
        private Visibility editableTitleBarVisibility;
        private string titleBarText;
        private string editableTitleBarText;
        private ItemModel selectedItem;

        public MainViewModel(IMainView view)
        {
            this.view = view;
            CustomIcon = nameof(PackIconKind.ArrowLeft);
            CustomCommand = new DelegateCommand(obj => ExpandOrCollapse());
            CloseCommand = new DelegateCommand(obj => Close());
            AddCommand = new DelegateCommand(obj => Add());
            SubmitCommand = new DelegateCommand(obj => Submit());
            Timer = new TimerViewModel();
            Timer.TimerFinished += OnTimerFinished;
            ReadOnlyTitleBarVisibility = Visibility.Visible;
            EditableTitleBarVisibility = Visibility.Collapsed;
            Items = new ObservableCollection<ItemModel>(App.Database.Items);
        }

        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                if (Equals(isExpanded, value)) return;
                isExpanded = value;
                CustomIcon = isExpanded ? nameof(PackIconKind.ArrowRight) : nameof(PackIconKind.ArrowLeft);
                view.Left = isExpanded ? workArea.Width - view.Width : workArea.Width - ExpansionOffset;
                view.Top = workArea.Height / 5;
            }
        }

        public string CustomIcon
        {
            get => customIcon;
            set
            {
                if (Equals(customIcon, value)) return;
                customIcon = value;
                OnPropertyChanged();
            }
        }

        public Visibility ReadOnlyTitleBarVisibility
        {
            get => readOnlyTitleBarVisibility;
            set
            {
                if (Equals(readOnlyTitleBarVisibility, value)) return;
                readOnlyTitleBarVisibility = value;
                OnPropertyChanged();
            }
        }

        public Visibility EditableTitleBarVisibility
        {
            get => editableTitleBarVisibility;
            set
            {
                if (Equals(editableTitleBarVisibility, value)) return;
                editableTitleBarVisibility = value;
                OnPropertyChanged();
            }
        }

        public string TitleBarText
        {
            get => titleBarText;
            set
            {
                if (Equals(titleBarText, value)) return;
                titleBarText = value;
                OnPropertyChanged();
            }
        }

        public string EditableTitleBarText
        {
            get => editableTitleBarText;
            set
            {
                if (Equals(editableTitleBarText, value)) return;
                editableTitleBarText = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ItemModel> Items { get; }

        public ItemModel SelectedItem
        {
            get => selectedItem;
            set
            {
                if (Equals(selectedItem, value)) return;
                selectedItem = value;
                if (selectedItem != null)
                {
                    OnSelectedItemChanged();
                }
                OnPropertyChanged();
            }
        }

        public ICommand CustomCommand { get; }

        public ICommand CloseCommand { get; }

        public ICommand AddCommand { get; }

        public ICommand SubmitCommand { get; }

        public void SetWorkArea(Rect workArea) => this.workArea = workArea;

        public TimerViewModel Timer { get; }

        private void OnTimerFinished(object sender, EventArgs e)
        {
            if (SelectedItem != null)
            {
                SelectedItem.Iterations++;
            }
        }

        private void ExpandOrCollapse() => IsExpanded = !IsExpanded;

        private void Add()
        {
            EditableTitleBarVisibility = Visibility.Visible;
            ReadOnlyTitleBarVisibility = Visibility.Collapsed;
            view.FocusEditableTitleBar();
        }

        private void Submit()
        {
            EditableTitleBarVisibility = Visibility.Collapsed;
            ReadOnlyTitleBarVisibility = Visibility.Visible;
            if (!string.IsNullOrEmpty(EditableTitleBarText))
            {
                var entry = App.Database.Items.Add(
                    new ItemModel
                    {
                        Title = EditableTitleBarText,
                        CreatedAt = DateTime.Now,
                    });
                App.Database.SaveChanges();
                var newItem = entry.Entity;
                Items.Add(newItem);
                TitleBarText = EditableTitleBarText;
                EditableTitleBarText = string.Empty;
                SelectedItem = newItem;
            }
        }

        private void OnSelectedItemChanged()
        {
            TitleBarText = SelectedItem.Title;
        }

        private void Close()
        {
            foreach (var item in Items)
            {
                App.Database.Update(item);
            }
            App.Database.SaveChanges();
            view.Close();
        }
    }
}
